using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Apis.Core.Model.Auth;
using AuthServiceApi.Controllers;
using AuthServiceApi.Repository;
using Common.IdentityApi;
using Common.IdentityApi.Login;
using Core;
using Core.Entity;
using Core.Entity.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace AuthServiceApi.Service;

public class AuthService(AuthServiceRepository authServiceRepository) {
    private AuthServiceRepository _authServiceRepository => authServiceRepository;
    private PasswordHasher<UserDto> _passwordHasher { get; } = new();
    private PasswordHasher<UserEntity> _passwordHasher2 { get; } = new();

    public async Task<HttpResult> RegisterUser(RegisterRequest request) {
        if (_authServiceRepository.Users.Get(u => u.Email == request.Email || u.UserName == request.Username)
                .FirstOrDefault() != null)
            // We can't provide an error message due to security reasons, return Ok and advise the user to check their email
            return new HttpResult {
                Succeeded = ResultType.Success,
            };

        var user = new UserEntity {
            UserName = request.Username,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
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
                }).ToArray() })
            .FirstOrDefault(u => u.UserName == loginRequest.Username || u.Email == loginRequest.Username);

        if (user == null)
            return new AuthenticationResult {
                Succeeded = false,
                Errors = new[] { "User not found" }
            };

        if (_passwordHasher.VerifyHashedPassword(user, user.Password!, loginRequest.Password) ==
            PasswordVerificationResult.Failed)
            return new AuthenticationResult {
                Succeeded = false,
                Errors = new[] { "Incorrect password" }
            };

        var result = await GenerateToken(user);

        return new AuthenticationResult {
            Succeeded = true,
            TokenResult = result
        };
    }

    public async Task<TokenResult> GenerateToken(UserDto user) {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Guid.NewGuid().ToString());

        var unitClaims = user.Units.Length == 0 ? "" : JsonSerializer.Serialize(user.Units.Select(u => new UnitDto
            { Id = u.Id, Name = u.Name }).ToList());
        var tokenDescriptor = new SecurityTokenDescriptor {
            Subject = new ClaimsIdentity(new[] {
                new Claim(CustomClaimTypes.Id, user.Id.ToString()),
                new Claim(CustomClaimTypes.Units, unitClaims)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var refreshToken = Guid.NewGuid().ToString();

        // _authServiceRepository.Tokens.Add(new UserToken {
        // UserId = user.Id,
        // LoginProvider = "jwt",
        // Name = "refresh",
        // Value = refreshToken
        // });

        var accessToken = tokenHandler.WriteToken(token);
        Console.WriteLine($"Access token: {accessToken}");
        return new TokenResult {
            AccessToken = tokenHandler.WriteToken(token),
            RefreshToken = refreshToken,
            ExpiresIn = tokenDescriptor.Expires?.ToString("yyyy-MM-ddTHH:mm:ss")
        };
    }

    public async Task LogoutUser() { }
}

public class UserDto {
    public UnitDto[] Units { get; set; }
    public Guid Id { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}

public class CustomClaimTypes {
    public const string Units = "units";
    public const string Id = "id";
}