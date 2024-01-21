using AutoMapper;
using Core.Entity;
using Core.Entity.Project;
using Core.RepositoryBase;

namespace ProjectServiceApi.Repository;

public class ProjectRepository(IMapper mapper,
                               RepositoryWithEntityId<ProjectEntity> projects) {
    public RepositoryWithEntityId<ProjectEntity> Projects { get; } = projects;
}
