using Common.Apis.Auth;
using Common.Model;
using Core.CustomHttpContext;
using Microsoft.AspNetCore.Mvc;
using ProjectServiceApi.Services;

namespace ProjectServiceApi.Controller;

[ApiController]
[Route("Api/[controller]")]
public class TicketController(TicketService _ticketService) : HttpControllerBase {
    [AppAuthorise]
    [HttpGet]
    public async Task<ActionResult<HttpResult<IEnumerable<TicketDto>>>> GetAll([FromQuery]Guid? projectId) {
        if(projectId != null) {
            return await GetProjectTickets(projectId);
        }
        
        var orgId = User.CurrentOrganisation;
        var result = await _ticketService.GetAll(orgId);
        return Ok(result);
    }
    
    private async Task<ActionResult<HttpResult<IEnumerable<TicketDto>>>> GetProjectTickets(Guid? projectId) {
        var orgId = User.CurrentOrganisation;
        if(projectId == null) {
            projectId = User.CurrentProject;
        } else if((User.Organisations.First(o => o.Id == orgId)?
                      .Projects)?.All(p => p.Id != projectId) ?? true) {
            return Forbid();
        }

        HttpResult<IEnumerable<TicketDto>> result = await _ticketService.GetByProjectId(projectId);
        return Ok(result);
    }

    [AppAuthorise]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<HttpResult<TicketDto>>> GetById(Guid id) {
        Console.WriteLine("Getting ticket by id");
        var orgId = User.CurrentOrganisation;
        HttpResult<TicketDto> result = await _ticketService.GetById(id, orgId);
        return Ok(result);
    }

    [AppAuthorise]
    [HttpPost]
    public async Task<ActionResult<HttpResult<TicketDto>>> Create(CreateTicketRequest ticketDto) {
        Console.WriteLine("Creating ticket");
        var orgId = User.CurrentOrganisation;
        ticketDto.OrganisationId = orgId;
        HttpResult<TicketDto> result = await _ticketService.Create(ticketDto);
        return Ok(result);
    }

    // Update a ticket
    [AppAuthorise]
    [HttpPut]
    public async Task<ActionResult<HttpResult<TicketDto>>> Update(UpdateTicketRequest ticket) {
        Console.WriteLine("Updating ticket");
        var orgId = User.CurrentOrganisation;
        if(ticket.OrganisationId != orgId) {
            return BadRequest();
        }

        if(ticket.ProjectId != null && User.Organisations.First(o => o.Id == orgId)
               .Projects.Any(p => p.Id == ticket.ProjectId)) {
            Console.WriteLine("User does not have access to this project");
            return Forbid();
        }

        var allUserProjectClaims = User.Organisations.SelectMany(o => o.Projects.Select(p => p.Id));

        HttpResult<TicketDto> result = await _ticketService.Update(ticket, allUserProjectClaims);
        if(result.Succeeded == ResultType.NotFound) {
            return NotFound(result);
        }

        if(result.Succeeded == ResultType.Forbidden) {
            return new OkObjectResult(result) {
                StatusCode = 403,
            };
        }

        return Ok(result);
    }

    // Delete a ticket
    [AppAuthorise]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<HttpResult<TicketDto>>> Delete(Guid id) {
        Console.WriteLine("Deleting ticket");
        var orgId = User.CurrentOrganisation;
        HttpResult<TicketDto> result = await _ticketService.Delete(id, orgId,
            User.Organisations.First(o => o.Id == orgId).Projects.Select(p => p.Id));
        return Ok(result);
    }
}
