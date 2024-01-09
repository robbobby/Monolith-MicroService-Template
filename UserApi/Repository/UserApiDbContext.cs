using Core;
using Core.Entity;
using Core.Entity.Identity;
using Microsoft.EntityFrameworkCore;

namespace UserApi.Repository;

public class UserApiDbContext(DbContextOptions<UserApiDbContext> options) : DbContext(options), IUserDbContext {
    public DbSet<UserUnitEntity> UserUnits { get; set; }
    public DbSet<UserEntity> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        Console.WriteLine($"Connection string: {Database.GetConnectionString()}");
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            if (entityType.ClrType.Name.EndsWith("Entity"))
                modelBuilder.Entity(entityType.ClrType).ToTable(entityType.ClrType.Name.Replace("Entity", ""));
    }

    private static string GetConnectionString() {
        return "Host=localhost;Database=WebApi;Username=rob;Port=5432";
    }
}