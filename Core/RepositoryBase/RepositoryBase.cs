using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Core.RepositoryBase;

public class RepositoryBase<T>(IAppDbContext dbContext,
                               IMapper mapper) where T : class {
    protected readonly IAppDbContext DbContext = dbContext;
    protected IMapper Mapper { get; } = mapper;

    public IQueryable<TU> GetAll<TU>() where TU : class {
        return DbContext.Set<T>()
            .ProjectTo<TU>(Mapper.ConfigurationProvider);
    }

    public IQueryable<T> GetAll() {
        return DbContext.Set<T>();
    }

    public void Add(T entity) {
        DbContext.Set<T>().Add(entity);
        SaveChanges();
    }

    public void Update(T entity) {
        DbContext.Set<T>().Update(entity);
        SaveChanges();
    }

    public void Delete(T entity) {
        DbContext.Set<T>().Remove(entity);
        SaveChanges();
    }

    public async Task Delete(Guid id) {
        var entity = await DbContext.Set<T>().FindAsync(id);

        if (entity != null) {
            DbContext.Set<T>().Remove(entity);
            await SaveChangesAsync();
        }
    }

    public void DeleteRange(IEnumerable<T> entities) {
        DbContext.Set<T>().RemoveRange(entities);
        SaveChanges();
    }

    public void AddRange(IEnumerable<T> entities) {
        DbContext.Set<T>().AddRange(entities);
        SaveChanges();
    }

    public Task<int> CountAsync() {
        return DbContext.Set<T>().CountAsync();
    }

    public int Count() {
        return DbContext.Set<T>().Count();
    }

    public bool Any(Expression<Func<T, bool>> predicate) {
        return DbContext.Set<T>().Any(predicate);
    }

    public T? FirstOrDefault(Expression<Func<T, bool>> condition) {
        return DbContext.Set<T>().FirstOrDefault(condition);
    }

    public void SaveChanges() {
        DbContext.SaveChanges();
    }

    public async Task SaveChangesAsync() {
        await DbContext.SaveChangesAsync();
    }

    public IEnumerable<T> Find(Expression<Func<T, bool>> condition) {
        return DbContext.Set<T>().Where(condition);
    }
}