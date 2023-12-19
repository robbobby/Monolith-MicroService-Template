using UnitApi.Model;
using UnitApi.Repository;
using AutoMapper;
using Core.Entity;

namespace UnitApi;

public class UnitApiMapperProfile : Profile {
    public UnitApiMapperProfile() {
        CreateMap<UnitEntity, UnitDto>();
    }
}