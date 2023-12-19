using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserApi.Model;
using UserApi.Service;

namespace UserApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(UserService userService, IMapper mapper)
    : ControllerBase {
    private IMapper _mapper { get; } = mapper;

    [HttpGet]
    [Route("users")]
    public Task<IActionResult> GetUsers() {
        var users = userService.GetAll<UserDto>();
        return Task.FromResult<IActionResult>(Ok(users));
    }
}