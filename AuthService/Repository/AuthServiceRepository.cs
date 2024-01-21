using Core.Entity;
using Core.Entity.Identity;
using Core.RepositoryBase;

namespace AuthServiceApi.Repository;

public class AuthServiceRepository(RepositoryWithEntityId<UserEntity> users, RepositoryBase<TokenEntity> tokens) {
    public RepositoryWithEntityId<UserEntity> Users { get; } = users;
    public RepositoryBase<TokenEntity> Tokens { get; } = tokens;
}
