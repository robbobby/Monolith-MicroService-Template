using Apis.Core.Model.Auth;
using AuthServiceApi.Service;
using Common.IdentityApi;
using Common.IdentityApi.Login;
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
    [HttpPost("Logout")]
    public async Task<IActionResult> Logout() {
        await authService.LogoutUser();
        return Ok();
    }

    [Authorize]
    [HttpPost("UpdateToken")]
    public async Task<ActionResult<HttpResult<TokenResult?>>> UpdateToken() {
        var userId = User.Claims.First(c => c.Type == CustomClaimType.UserId).Value;
        var result = await authService.UpdateToken(userId);

        return Ok(new HttpResult<TokenResult> {
            Succeeded = ResultType.Success,
            Data = result
        });
    }
}

public class AuthenticationResult {
    public TokenResult? TokenResult { get; set; }
    public bool Succeeded { get; set; }
    public string[] Errors { get; set; }
}