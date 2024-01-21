using AutoMapper;
using Common.Apis.Auth;
using Common.Apis.Project;
using Core.Entity;
using Core.Entity.Project;
using ProjectServiceApi.Repository;

namespace ProjectServiceApi.Services;

public class ProjectService(ProjectRepository _projectRepository, IMapper _mapper) {
    public IReadOnlyList<ProjectEntity> GetAll(Guid organisation) {
        return _projectRepository.Projects.GetAll().Where(x => x.OrganisationId == organisation).ToList();
    }

    public ProjectEntity? Get(Guid id) {
        return _projectRepository.Projects.GetAll().FirstOrDefault(x => x.Id == id);
    }

    public Task<HttpResult<Guid>?> Create(ProjectCreateRequest project) {
        var projectEntity = _mapper.Map<ProjectEntity>(project);
        var result = _projectRepository.Projects.CreateAsync(projectEntity);
        if(result.Result == null)
            return Task.FromResult<HttpResult<Guid>?>(new HttpResult<Guid> {
                Succeeded = ResultType.Error
            });

        return Task.FromResult<HttpResult<Guid>?>(new HttpResult<Guid> {
            Succeeded = ResultType.Success,
            Data = projectEntity.Id
        });
    }
}
