using Microsoft.AspNetCore.Mvc;
using UnitApi.Model;
using UnitApi.Service;

namespace UnitApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UnitController(UnitService unitService) : ControllerBase {
    [HttpGet]
    [Route("units")]
    public Task<IActionResult> GetUnits() {
        var result = unitService.GetAll<UnitDto>().ToList();
        return Task.FromResult<IActionResult>(Ok());
    }
}