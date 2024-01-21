using AuthServiceApi.Controllers;
using AuthServiceApi.Repository;
using Common.Apis.Auth;
using Common.Apis.Auth.Login;
using Common.Apis.Auth.Register;
using Core;
using Core.Entity.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthServiceApi.Service;

public class AuthService(AuthServiceRepository authServiceRepository, TokenService _tokenService) {
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

        var result = await _authServiceRepository.Users.CreateAsync(user);

        return new HttpResult {
            Succeeded = ResultType.Success
        };
    }

    public async Task<AuthenticationResult> AuthenticateUser(LoginRequest loginRequest) {
        var user = GetUser().FirstOrDefault(u => u.UserName == loginRequest.Username);

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

        var result = _tokenService.GenerateToken(user);

        return new AuthenticationResult {
            Succeeded = true,
            TokenResult = result
        };
    }

    public async Task LogoutUser(string userId, string refreshToken) {
        var token = _authServiceRepository.Tokens
            .Get(t => t.UserId.ToString() == userId && t.RefreshToken == refreshToken).FirstOrDefault();
        if(token == null) return;
        await _authServiceRepository.Tokens.DeleteAsync(token);
    }

    public Task<TokenResult> UpdateToken(string userId, Guid? organisationId, Guid? projectId) {
        var user = GetUser().FirstOrDefault(u => u.Id.ToString() == userId);
        if(user == null) throw new Exception("User not found");
        return Task.FromResult(_tokenService.GenerateToken(user, organisationId, projectId));
    }

    private IQueryable<UserDto> GetUser() {
        return _authServiceRepository.Users
            .Get()
            .Include(u => u.Organisations)
            .ThenInclude(uu => uu.Organisation)
            .ThenInclude(o => o.Projects)
            .Select(u => new UserDto {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                Password = u.PasswordHash,
                Organisations = u.Organisations.Select(uu => new OrganisationDto {
                    Id = uu.Organisation.Id,
                    Name = uu.Organisation.Name,
                    Projects = uu.Organisation.Projects.Select(p => new ProjectDto {
                        Id = p.Id,
                        Name = p.Name
                    }).ToArray()
                }).ToArray()
            });
    }
}

public class UserDto {
    public OrganisationDto[] Organisations { get; set; }
    public Guid Id { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}
