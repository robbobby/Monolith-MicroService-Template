using AuthServiceApi.Service;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthServiceApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthServiceController(AuthServiceService authServiceService, IMapper mapper)
    : ControllerBase {
    private readonly IMapper _mapper = mapper;

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterRequest reqBody) {
        var result = await authServiceService.RegisterUser(reqBody);

        if (result.Succeeded == ResultType.Success) return Ok();

        if (result.Succeeded == ResultType.Conflict) return Conflict(result);
        if (result.Errors.Length > 0) return BadRequest(result);

        return BadRequest(result);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<AuthenticationResult>> LoginWithPassword([FromBody] UserLoginRequest reqBody) {
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

public class UserLoginRequest {
    public string Username { get; set; }
    public string Password { get; set; }
}

public class UserRegisterRequest {
    public string Username { get; set; }
    public string Password { get; set; }    
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}