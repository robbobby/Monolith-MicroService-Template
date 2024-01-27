using AutoMapper;
using Core.Entity.Identity;
using OrganisationApi.Model;

namespace OrganisationApi;

public class OrganisationApiMapperProfile : Profile {
    public OrganisationApiMapperProfile() {
        CreateMap<OrganisationEntity, OrganisationDto>();
    }
}
