using AuthServiceApi.Service;
using Common.Apis.Auth;
using Common.Apis.Auth.Login;
using Common.Apis.Auth.Register;
using Common.Apis.Auth.UpdateToken;
using Common.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthServiceApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(AuthService authService)
    : ControllerBase {
    [AllowAnonymous]
    [HttpPost("Register")]
    public async Task<ActionResult<HttpResult>> Register([FromBody] RegisterRequest reqBody) {
        var result = await authService.RegisterUser(reqBody);

        if(result.Succeeded == ResultType.Success) return Ok(new HttpResult { Succeeded = ResultType.Success });

        if(result.Errors.Length > 0) return BadRequest(result);

        return BadRequest(result);
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<ActionResult<HttpResult<TokenResult?>>> LoginWithPassword([FromBody] LoginRequest reqBody) {
        var result = await authService.AuthenticateUser(reqBody);
        if(result.Succeeded)
            return Ok(new HttpResult<TokenResult> {
                Succeeded = ResultType.Success,
                Data = result.TokenResult
            });

        return BadRequest(new HttpResult {
            Succeeded = ResultType.Failure,
            Errors = result.Errors
        });
    }

    [Authorize]
    [HttpPost("SignOut")]
    public async Task<IActionResult> Logout([FromBody] string refreshToken) {
        try {
            await authService.LogoutUser(User.Claims.First(c => c.Type == CustomClaimType.UserId).Value,
                refreshToken);

            return Ok(new HttpResult {
                Succeeded = ResultType.Success
            });
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return BadRequest(new HttpResult {
                Succeeded = ResultType.Failure,
                Errors = ["Oops, something went wrong!"]
            });
        }
    }

    [Authorize]
    [HttpPost("UpdateToken")]
    public async Task<ActionResult<HttpResult<TokenResult?>>> UpdateToken([FromBody] UpdateTokenRequest body) {
        var userId = User.Claims.First(c => c.Type == CustomClaimType.UserId).Value;
        var result = await authService.UpdateToken(userId, body.OrganisationId, body.ProjectId);

        return Ok(new HttpResult<TokenResult> {
            Succeeded = ResultType.Success,
            Data = result
        });
    }
}

public class AuthenticationResult {
    public TokenResult? TokenResult { get; init; }
    public bool Succeeded { get; init; }
    public string[] Errors { get; init; } = [];
}
