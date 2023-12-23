using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Entity.Interface;
using Microsoft.EntityFrameworkCore;

namespace Core.RepositoryBase;

public class RepositoryWithEntityId<T>(IAppDbContext dbContext,
                                       IMapper mapper) :
    RepositoryBase<T>(dbContext, mapper) where T : class, IEntityId {
    public IQueryable<T> Get(Guid id) {
        return DbContext.Set<T>().Where(e => e.Id == id);
    }

    public IQueryable<TU> Get<TU>(Guid id) where TU : class {
        return DbContext.Set<T>().Where(e => e.Id == id)
            .ProjectTo<TU>(Mapper.ConfigurationProvider);
    }
}

public interface IAppDbContext {
    DbSet<T> Set<T>() where T : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    int SaveChanges();
}