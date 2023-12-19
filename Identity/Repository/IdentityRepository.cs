using AutoMapper;
using Core.Entity;
using Core.RepositoryBase;

namespace IdentityApi.Repository;

public class IdentityRepository(IdentityApiDbContext dbContext, IMapper mapper)
    : RepositoryWithEntityId<IdentityEntity>(dbContext, mapper);