using IdentityApi.Model;
using IdentityApi.Repository;
using AutoMapper;
using Core.Entity;

namespace IdentityApi;

public class IdentityApiMapperProfile : Profile {
    public IdentityApiMapperProfile() {
        CreateMap<IdentityEntity, IdentityDto>();
    }
}