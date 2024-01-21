using AutoMapper;
using Common.Apis.Project;
using Core.Entity;

namespace ProjectServiceApi;

public class ProjectApiMapperProfile : Profile {
    public ProjectApiMapperProfile() {
        CreateMap<ProjectCreateRequest, ProjectEntity>();
    }
}
