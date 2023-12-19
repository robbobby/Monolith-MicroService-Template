using AutoMapper;
using Core;
using Core.Entity;
using Core.RepositoryBase;

namespace RepositoryLayer.Repository;

public class UnitRepository(AppDbContext dbContext, IMapper mapper) : RepositoryWithEntityId<Unit>(dbContext, mapper);