using AutoMapper;
using Core.Entity;
using Core.Entity.Identity;
using UnitApi.Model;

namespace UnitApi;

public class UnitApiMapperProfile : Profile {
    public UnitApiMapperProfile() {
        CreateMap<OrganisationEntity, UnitDto>();
    }
}
