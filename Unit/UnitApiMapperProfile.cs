using AutoMapper;
using Core.Entity;
using UnitApi.Model;

namespace UnitApi;

public class UnitApiMapperProfile : Profile {
    public UnitApiMapperProfile() {
        CreateMap<OrganisationEntity, UnitDto>();
    }
}