using AutoMapper;
using Core.Entity.Identity;
using UserApi.Model;

namespace UserApi;

public class UserApiMapperProfile : Profile {
    public UserApiMapperProfile() {
        CreateMap<UserEntity, UserDto>();
        CreateMap<UserOrganisationEntity, OrganisationDto>();
    }
}
