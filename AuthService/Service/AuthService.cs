using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Apis.Core.Model.Auth;
using AuthServiceApi.Controllers;
using AuthServiceApi.Repository;
using Common.IdentityApi;
using Common.IdentityApi.Login;
using Common.Model;
using Core;
using Core.Entity;
using Core.Entity.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace AuthServiceApi.Service;

public class AuthService(AuthServiceRepository authServiceRepository, IConfiguration _configuration) {
    private AuthServiceRepository _authServiceRepository => authServiceRepository;
    private PasswordHasher<UserDto> _passwordHasher { get; } = new();
    private PasswordHasher<UserEntity> _passwordHasher2 { get; } = new();

    public async Task<HttpResult> RegisterUser(RegisterRequest request) {
        if(_authServiceRepository.Users.Get(u => u.Email == request.Email || u.UserName == request.Username)
               .FirstOrDefault() != null)
            // We can't provide an error message due to security reasons, return Ok and advise the user to check their email
            return new HttpResult {
                Succeeded = ResultType.Success
            };

        var user = new UserEntity {
            UserName = request.Username,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        user.PasswordHash = _passwordHasher2.HashPassword(user, request.Password);

        var result = await _authServiceRepository.Users.AddAsync(user);

        return new HttpResult {
            Succeeded = ResultType.Success
        };
    }

    public async Task<AuthenticationResult> AuthenticateUser(LoginRequest loginRequest) {
        var user = _authServiceRepository.Users
            .Get()
            .Select(u => new UserDto {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                Password = u.PasswordHash,
                Units = u.Organisations.Select(uu => new UnitDto {
                    Id = uu.Organisation.Id,
                    Name = uu.Organisation.Name
                }).ToArray()
            })
            .FirstOrDefault(u => u.UserName == loginRequest.Username || u.Email == loginRequest.Username);

        if(user == null)
            return new AuthenticationResult {
                Succeeded = false,
                Errors = ["Incorrect username or password"]
            };

        if(_passwordHasher.VerifyHashedPassword(user, user.Password!, loginRequest.Password) ==
           PasswordVerificationResult.Failed)
            return new AuthenticationResult {
                Succeeded = false,
                Errors = ["Incorrect username or password"]
            };

        var result = GenerateToken(user);

        return new AuthenticationResult {
            Succeeded = true,
            TokenResult = result
        };
    }

    private TokenResult GenerateToken(UserDto user, Guid? organisationId = null) {
        var claims = GetClaims(user, organisationId);

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
        _authServiceRepository.Tokens.Add(new TokenEntity {
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

    private ClaimsIdentity GetClaims(UserDto user, Guid? organisationId) {
        var claims = new ClaimsIdentity(new[] {
            new Claim(JwtRegisteredClaimNames.Iss, _configuration["Identity:Authority"]!),
            new Claim(JwtRegisteredClaimNames.Aud, _configuration["Identity:Audience"]!),
            new Claim(CustomClaimType.UserId, user.Id.ToString())
        });

        var unitClaims = user.Units.Length == 0
            ? "[]"
            : JsonSerializer.Serialize(user.Units.Select(u => new UnitDto
                { Id = u.Id, Name = u.Name, Role = u.Role }).ToArray());

        claims.AddClaim(new Claim(CustomClaimType.Organisations, unitClaims));

        if(organisationId != null) {
            var unit = user.Units.FirstOrDefault(u => u.Id == organisationId);
            if(unit != null)
                claims.AddClaim(new Claim(CustomClaimType.CurrentOrganisationId, organisationId.ToString()));
        } else if(user.Units.Length > 0) {
            claims.AddClaim(new Claim(CustomClaimType.CurrentOrganisationId, user.Units[0].Id.ToString()));
        }

        return claims;
    }

    public async Task LogoutUser(string userId, string refreshToken) {
        var token = _authServiceRepository.Tokens
            .Get(t => t.UserId.ToString() == userId && t.RefreshToken == refreshToken).FirstOrDefault();
        if(token == null) return;
        await _authServiceRepository.Tokens.DeleteAsync(token);
    }

    public Task<TokenResult> UpdateToken(string userId) {
        return Task.FromResult(GenerateToken(_authServiceRepository.Users
            .Get()
            .Select(u => new UserDto {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                Password = u.PasswordHash,
                Units = u.Organisations.Select(uu => new UnitDto {
                    Id = uu.Organisation.Id,
                    Name = uu.Organisation.Name
                }).ToArray()
            })
            .FirstOrDefault(u => u.Id.ToString() == userId)!));
    }
}

public class UserDto {
    public UnitDto[] Units { get; set; }
    public Guid Id { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}