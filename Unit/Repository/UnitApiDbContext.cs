using Core.Entity;
using Core.RepositoryBase;
using Microsoft.EntityFrameworkCore;

namespace UnitApi.Repository;

public class UnitApiDbContext(DbContextOptions<UnitApiDbContext> options) : DbContext(options), IAppDbContext {
    public DbSet<OrganisationEntity> Units { get; set; }
    public DbSet<UserOrganisationEntity> UserUnits { get; set; }
}
