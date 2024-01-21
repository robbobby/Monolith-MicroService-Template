using Core.Entity;
using Core.Entity.Identity;
using Core.Entity.Project;
using Core.RepositoryBase;
using Microsoft.EntityFrameworkCore;

namespace Core;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext,
        IAppDbContext, IUserDbContext {
    public DbSet<OrganisationEntity> Units { get; set; }
    public DbSet<UserOrganisationEntity> UserUnits { get; set; }
    public DbSet<TokenEntity> Tokens { get; set; }
    public DbSet<ProjectEntity> Projects { get; set; }
    public DbSet<UserEntity> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.UseNpgsql("Host=localhost;Database=WebApi;Username=rob;Port=5432");
    }
}

public interface IUserDbContext : IAppDbContext {
    DbSet<UserEntity> Users { get; set; }
}
