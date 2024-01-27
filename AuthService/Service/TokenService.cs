using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using AuthServiceApi.Repository;
using Common.Apis.Auth.Login;
using Common.Model;
using Core;
using Core.Entity.Identity;
using Microsoft.IdentityModel.Tokens;

namespace AuthServiceApi.Service;

public class TokenService(AuthServiceRepository _authServiceRepository, IConfiguration _configuration) {
    public TokenResult GenerateToken(UserDto user, Guid? organisationId = null, Guid? projectId = null) {
        var claims = GetClaims(user, organisationId, projectId);

        var key = Encoding.ASCII.GetBytes(_configuration["Identity:Key"]!);
        var expiry = DateTime.UtcNow.AddHours(1);

        var tokenDescriptor = new SecurityTokenDescriptor {
            Subject = claims,
            Expires = expiry,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var refreshToken = Guid.NewGuid().ToString();

        var accessToken = tokenHandler.WriteToken(token);

        var expiresAt = DateTime.UtcNow.AddDays(7).ToString("yyyy-MM-ddTHH:mm:ss");
        _authServiceRepository.Tokens.Create(new TokenEntity {
            UserId = user.Id,
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresAt = expiresAt
        });

        return new TokenResult {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresIn = expiresAt
        };
    }

    private ClaimsIdentity GetClaims(UserDto user, Guid? organisationId, Guid? projectId) {
        var claims = new ClaimsIdentity(new[] {
            new Claim(JwtRegisteredClaimNames.Iss, _configuration["Identity:Authority"]!),
            new Claim(JwtRegisteredClaimNames.Aud, _configuration["Identity:Audience"]!),
            new Claim(CustomClaimType.UserId, user.Id.ToString())
        });

        var organisationClaims = user.Organisations.Length == 0
            ? "[]"
            : JsonSerializer.Serialize(user.Organisations.Select(u => new OrganisationDto
                { Id = u.Id, Name = u.Name, Role = u.Role, Projects = u.Projects }).ToArray());

        claims.AddClaim(new Claim(CustomClaimType.Organisations, organisationClaims));


        if(organisationId != null) {
            var organisation = user.Organisations.FirstOrDefault(u => u.Id == organisationId);
            if(organisation != null) {
                claims.AddClaim(new Claim(CustomClaimType.OrganisationId, organisationId.ToString()));
                if(projectId != null) {
                    var project = organisation.Projects.FirstOrDefault(p => p.Id == projectId);
                    if(project != null) claims.AddClaim(new Claim(CustomClaimType.ProjectId, projectId.ToString()));
                } else if(organisation.Projects.Count > 0) {
                    claims.AddClaim(new Claim(CustomClaimType.ProjectId, organisation.Projects[0].Id.ToString()));
                }
            }
        } else if(user.Organisations.Length > 0) {
            var orgWithProject = user.Organisations.FirstOrDefault(u => u.Projects.Count > 0);
            if(orgWithProject != null) {
                claims.AddClaim(new Claim(CustomClaimType.OrganisationId, orgWithProject.Id.ToString()));
                claims.AddClaim(new Claim(CustomClaimType.ProjectId, orgWithProject.Projects[0].Id.ToString()));
            } else {
                claims.AddClaim(new Claim(CustomClaimType.OrganisationId, user.Organisations[0].Id.ToString()));
            }
        }

        return claims;
    }
}
