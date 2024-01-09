using Api.Core.Model.Auth;
using AuthServiceApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RegisterRequest = Api.Core.Model.Auth.RegisterRequest;

namespace AuthServiceApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(AuthServiceService authServiceService)
    : ControllerBase {

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest reqBody) {
        Console.WriteLine(reqBody);
        var result = await authServiceService.RegisterUser(reqBody);

        if (result.Succeeded == ResultType.Success) return Ok();

        if (result.Succeeded == ResultType.Conflict) return Conflict(result);
        if (result.Errors.Length > 0) return BadRequest(result);

        return BadRequest(result);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<AuthenticationResult>> LoginWithPassword([FromBody] LoginRequest reqBody) {
        var result = await authServiceService.AuthenticateUser(reqBody);

        if (result.Succeeded) return Ok(result);

        return Ok(result);
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout() {
        await authServiceService.LogoutUser();
        return Ok();
    }
}

public class AuthenticationResult {
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public bool Succeeded { get; set; }
    public string[] Errors { get; set; }
}
