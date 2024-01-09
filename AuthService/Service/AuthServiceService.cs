using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Apis.Core.Model.Auth;
using AuthServiceApi.Controllers;
using AuthServiceApi.Repository;
using Common.IdentityApi;
using Common.IdentityApi.Login;
using Core.Entity.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace AuthServiceApi.Service;

public class AuthServiceService(AuthServiceRepository authServiceRepository) {
    private AuthServiceRepository _authServiceRepository => authServiceRepository;
    private PasswordHasher<UserEntity> _passwordHasher { get; } = new();

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

        user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

        var result = await _authServiceRepository.Users.AddAsync(user);

        return new HttpResult {
            Succeeded = ResultType.Success
        };
    }

    public async Task<AuthenticationResult> AuthenticateUser(LoginRequest loginRequest) {
        var user = _authServiceRepository.Users
            .Get(u => u.UserName == loginRequest.Username || u.Email == loginRequest.Username).FirstOrDefault();

        if (user == null)
            return new AuthenticationResult {
                Succeeded = false,
                Errors = new[] { "User not found" }
            };

        if (_passwordHasher.VerifyHashedPassword(user, user.PasswordHash!, loginRequest.Password) ==
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

    public async Task<TokenResult> GenerateToken(UserEntity user) {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Guid.NewGuid().ToString());

        var tokenDescriptor = new SecurityTokenDescriptor {
            Subject = new ClaimsIdentity(new[] {
                // new Claim(CustomClaimTypes.Units,
                    // user.Units.Select(u => u.UnitId.ToString()).Aggregate((a, b) => $"{a},{b}")),
                new Claim(CustomClaimTypes.Id, user.Id.ToString())
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

        return new TokenResult {
            AccessToken = tokenHandler.WriteToken(token),
            RefreshToken = refreshToken,
            ExpiresIn = tokenDescriptor.Expires?.ToString("yyyy-MM-ddTHH:mm:ss")
        };
    }

    public async Task LogoutUser() { }
}

public class CustomClaimTypes {
    public const string Units = "units";
    public const string Id = "id";
}
