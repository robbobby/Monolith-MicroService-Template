using AutoMapper;
using Core.CustomHttpContext;
using Microsoft.AspNetCore.Mvc;
using UserApi.Service;

namespace UserApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(UserService userService,
                            IMapper mapper)
    : HttpControllerBase {
    private IMapper _mapper { get; } = mapper;

    [AppAuthorise()]
    [HttpGet]
    [Route("users")]
    public Task<IActionResult> GetUsers() {
        // TODO: THe map is throwing an error for some reason
        var users = userService.GetAll(User.CurrentOrganisation);
        return Task.FromResult<IActionResult>(Ok(users));
    }
}