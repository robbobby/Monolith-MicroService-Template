using AutoMapper;
using Core.Entity;
using Core.RepositoryBase;

namespace UnitApi.Repository;

public class UnitRepository(UnitApiDbContext dbContext, IMapper mapper)
    : RepositoryWithEntityId<UnitEntity>(dbContext, mapper);