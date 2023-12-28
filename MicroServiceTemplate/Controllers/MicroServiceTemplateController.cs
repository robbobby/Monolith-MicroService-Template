using AutoMapper;
using MicroServiceTemplateApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace MicroServiceTemplateApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MicroServiceTemplateController(MicroServiceTemplateService microservicetemplateService, IMapper mapper)
    : ControllerBase {
    private IMapper _mapper { get; } = mapper;

    [HttpGet]
    [Route("microservicetemplates")]
    public Task<IActionResult> GetMicroServiceTemplates() {
        return Task.FromResult<IActionResult>(Ok());
    }
}