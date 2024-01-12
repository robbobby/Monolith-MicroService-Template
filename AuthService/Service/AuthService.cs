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
                Units = u.Units.Select(uu => new UnitDto {
                    Id = uu.Unit.Id,
                    Name = uu.Unit.Name
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

    private TokenResult GenerateToken(UserDto user) {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Identity:Key"]!);

        var unitClaims = user.Units.Length == 0
            ? ""
            : JsonSerializer.Serialize(user.Units.Select(u => new UnitDto
                { Id = u.Id, Name = u.Name }).ToList());

        var expiry = DateTime.UtcNow.AddHours(1);
        var tokenDescriptor = new SecurityTokenDescriptor {
            Subject = new ClaimsIdentity(new[] {
                new Claim(CustomClaimType.UserId, user.Id.ToString()),
                new Claim(CustomClaimType.Units, unitClaims),
                new Claim(JwtRegisteredClaimNames.Iss, _configuration["Identity:Authority"]!),
                new Claim(JwtRegisteredClaimNames.Aud, _configuration["Identity:Audience"]!)
            }),
            Expires = expiry,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var refreshToken = Guid.NewGuid().ToString();

        var accessToken = tokenHandler.WriteToken(token);
        _authServiceRepository.Tokens.Add(new TokenEntity {
            UserId = user.Id,
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddDays(7).ToString("yyyy-MM-ddTHH:mm:ss")
        });

        return new TokenResult {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresIn = tokenDescriptor.Expires?.ToString("yyyy-MM-ddTHH:mm:ss")
        };
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
                Units = u.Units.Select(uu => new UnitDto {
                    Id = uu.Unit.Id,
                    Name = uu.Unit.Name
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