using Common.Apis.Auth;
using Common.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UnitApi.Model;
using UnitApi.Service;

namespace UnitApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UnitController(UnitService unitService) : ControllerBase {
    [HttpGet]
    public Task<IActionResult> GetUnits() {
        var result = unitService.GetAll<UnitDto>().ToList();
        return Task.FromResult<IActionResult>(Ok());
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<HttpResult<Guid>>> CreateUnit([FromBody] string name) {
        var userId = Guid.Parse(User.Claims.First(c => c.Type == CustomClaimType.UserId).Value);
        var result = await unitService.Create(name, userId);
        return Ok(new HttpResult<Guid?> {
            Succeeded = ResultType.Success,
            Data = result
        });
    }
}
