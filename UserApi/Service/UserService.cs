using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Entity.Identity;
using UserApi.Repository;

namespace UserApi.Service;

public class UserService(UserRepository userRepository, IMapper mapper) {
    public IMapper Mapper { get; } = mapper;

    public IReadOnlyList<T> GetAll<T>(Guid orgId) where T : class {
        return userRepository.Users.GetAll<UserEntity>()
            .Where(u => u.Organisations.Any(o => o.OrganisationId == orgId))
            .ProjectTo<T>(mapper.ConfigurationProvider).ToList();
    }

    public IReadOnlyList<UserEntity> GetAll(Guid orgId) {
        return userRepository.Users.GetAll()
            .Where(u => u.Organisations.Any(o => o.OrganisationId == orgId))
            .ToList();
    }
}
