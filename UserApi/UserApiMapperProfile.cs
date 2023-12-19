using AutoMapper;
using Core.Entity;
using UserApi.Model;
using UnitDto = UserApi.Model.UnitDto;

namespace UserApi;

public class UserApiMapperProfile: Profile {
    public UserApiMapperProfile() {
        CreateMap<UserEntity, UserDto>();
        CreateMap<UserUnitEntity, UnitDto>();
    }
}
