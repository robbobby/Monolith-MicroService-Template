using AutoMapper;
using Common.Apis.Project;
using Core.Entity;
using Core.Entity.Project;

namespace ProjectServiceApi;

public class ProjectApiMapperProfile : Profile {
    public ProjectApiMapperProfile() {
        CreateMap<ProjectCreateRequest, ProjectEntity>();
    }
}
