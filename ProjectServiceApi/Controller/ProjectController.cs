using Common.Apis.Auth;
using Common.Apis.Project;
using Core.CustomHttpContext;
using Microsoft.AspNetCore.Mvc;
using ProjectServiceApi.Services;

namespace ProjectServiceApi.Controller;

[ApiController]
[Route("api/[controller]")]
public class ProjectController(ProjectService _projectService) : HttpControllerBase {
    [AppAuthorise]
    [HttpGet]
    public Task<IActionResult> GetProjects() {
        var projects = _projectService.GetAll(User.CurrentOrganisation);
        return Task.FromResult<IActionResult>(Ok(projects));
    }

    [AppAuthorise]
    [HttpGet]
    [Route("{id}")]
    public Task<IActionResult> GetProject(Guid id) {
        var project = _projectService.Get(id);
        return Task.FromResult<IActionResult>(Ok(project));
    }

    [AppAuthorise]
    [HttpPost]
    public Task<ActionResult<Guid>> CreateProject([FromBody] ProjectCreateRequest project) {
        project.OrganisationId = User.CurrentOrganisation;
        var createdProject = _projectService.Create(project);

        if(createdProject.Result?.Succeeded != ResultType.Success)
            return Task.FromResult<ActionResult<Guid>>(BadRequest(createdProject));

        return Task.FromResult<ActionResult<Guid>>(Ok(createdProject));
    }
}
