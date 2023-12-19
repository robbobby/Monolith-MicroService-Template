using AutoMapper;
using Core;
using Core.Entity;

namespace UserApi;

public class UserApiMapperProfile: Profile {
    public UserApiMapperProfile() {
        CreateMap<User, UserDto>();
        CreateMap<UserUnit, UnitDto>();
    }
}
