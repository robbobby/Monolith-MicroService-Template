using Core.Entity.Identity;
using Core.Entity.Project;
using Core.RepositoryBase;
using Microsoft.EntityFrameworkCore;

namespace Core;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext,
        IAppDbContext, IUserDbContext {
    public DbSet<OrganisationEntity> Organisation { get; init; } = null!;
    public DbSet<UserOrganisationEntity> UserOrganisations { get; init; } = null!;
    public DbSet<TokenEntity> Tokens { get; init; } = null!;
    public DbSet<ProjectEntity> Projects { get; init; } = null!;
    public DbSet<UserEntity> Users { get; init; } = null!;
    public DbSet<TicketEntity> Tickets { get; init; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.UseNpgsql("Host=localhost;Database=WebApi;Username=rob;Port=5432");
    }
}

public interface IUserDbContext : IAppDbContext {
    DbSet<UserEntity> Users { get; init; }
}
