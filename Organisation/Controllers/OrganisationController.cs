using Common.Apis.Auth;
using Common.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganisationApi.Model;
using OrganisationApi.Service;

namespace OrganisationApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrganisationController(OrganisationService organisationService) : ControllerBase {
    [HttpGet]
    public Task<IActionResult> GetOrganisations() {
        var result = organisationService.GetAll<OrganisationDto>().ToList();
        return Task.FromResult<IActionResult>(Ok());
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<HttpResult<Guid>>> CreateOrganisation([FromBody] string name) {
        var userId = Guid.Parse(User.Claims.First(c => c.Type == CustomClaimType.UserId).Value);
        var result = await organisationService.Create(name, userId);
        return Ok(new HttpResult<Guid?> {
            Succeeded = ResultType.Success,
            Data = result
        });
    }
}
