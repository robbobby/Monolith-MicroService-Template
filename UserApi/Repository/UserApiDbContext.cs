using Core;
using Core.Entity.Identity;
using Microsoft.EntityFrameworkCore;

namespace UserApi.Repository;

public class UserApiDbContext(DbContextOptions<UserApiDbContext> options) : DbContext(options), IUserDbContext {
    public DbSet<UserOrganisationEntity> UserOrganisations { get; init; } = null!;
    public DbSet<UserEntity> Users { get; init; } = null!;
}
