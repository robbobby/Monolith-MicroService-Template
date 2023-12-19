using Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace UserApi.Repository;

public class UserApiDbContext(DbContextOptions<UserApiDbContext> options) : DbContext(options) {
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<UserUnitEntity> UserUnits { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes()) {
            if (entityType.ClrType.Name.EndsWith("Entity")) {
                modelBuilder.Entity(entityType.ClrType).ToTable(entityType.ClrType.Name.Replace("Entity", ""));
            }
        }
    }
}