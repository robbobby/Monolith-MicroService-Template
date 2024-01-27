using AutoMapper;
using Common.Apis.Project;
using Common.Model;
using Core.Entity.Project;

namespace ProjectServiceApi;

public class ProjectApiMapperProfile : Profile {
    public ProjectApiMapperProfile() {
        CreateMap<ProjectCreateRequest, ProjectEntity>();
        
        CreateMap<TicketDto, TicketEntity>();
        CreateMap<TicketEntity, TicketDto>();
        CreateMap<CreateTicketRequest, TicketEntity>();
        CreateMap<UpdateTicketRequest, TicketEntity>();
        CreateMap<UpdateTicketRequest, TicketDto>();
    }
}
