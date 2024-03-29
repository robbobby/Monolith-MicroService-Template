using Core.Entity.Identity;
using Core.RepositoryBase;
using Microsoft.EntityFrameworkCore;

namespace AuthServiceApi.Repository;

public class AuthServiceApiDbContext(DbContextOptions<AuthServiceApiDbContext> options)
    : DbContext(options), IAppDbContext {
    public DbSet<UserEntity> Users { get; set; } = null!;
    public DbSet<TokenEntity> Tokens { get; set; } = null!;
}
