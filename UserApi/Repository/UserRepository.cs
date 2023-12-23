using AutoMapper;
using Core;
using Core.Entity.Identity;
using Core.RepositoryBase;

namespace UserApi.Repository;

public class UserRepository(IUserDbContext dbContext,
                            IMapper mapper)
    : RepositoryBase<UserEntity>(dbContext, mapper);