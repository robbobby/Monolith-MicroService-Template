using Core.Entity.Identity;
using Core.RepositoryBase;

namespace UserApi.Repository;

public class UserRepository(RepositoryWithEntityId<UserEntity> users) {
    public RepositoryWithEntityId<UserEntity> Users { get; } = users;
}
