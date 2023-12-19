using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using IdentityApi.Service;

namespace IdentityApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IdentityController(IdentityService identityService, IMapper mapper)
    : ControllerBase {
    private IMapper _mapper { get; } = mapper;

    [HttpGet]
    [Route("identitys")]
    public Task<IActionResult> GetIdentitys() {
        return Task.FromResult<IActionResult>(Ok());
    }
}