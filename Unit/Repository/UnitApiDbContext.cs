using Core.Entity;
using Core.RepositoryBase;
using Microsoft.EntityFrameworkCore;

namespace UnitApi.Repository;

public class UnitApiDbContext(DbContextOptions<UnitApiDbContext> options) : DbContext(options), IAppDbContext {
    public DbSet<UnitEntity> Units { get; set; }
    public DbSet<UserUnitEntity> UserUnits { get; set; }
}