using Core;
using Core.Entity;
using Core.Entity.Identity;
using Microsoft.EntityFrameworkCore;

namespace UserApi.Repository;

public class UserApiDbContext(DbContextOptions<UserApiDbContext> options) : DbContext(options), IUserDbContext {
    public DbSet<UserOrganisationEntity> UserUnits { get; set; }
    public DbSet<UserEntity> Users { get; set; }
}
