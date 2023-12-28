using Core.Entity.Identity;
using Core.RepositoryBase;

namespace AuthServiceApi.Repository;

public class AuthServiceRepository(RepositoryWithEntityId<UserEntity> users, RepositoryBase<UserToken> tokens) {
    public RepositoryWithEntityId<UserEntity> Users { get; } = users;
    public RepositoryBase<UserToken> Tokens { get; } = tokens;
}