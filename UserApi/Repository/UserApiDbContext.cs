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
    }

    private static string GetConnectionString() {
        return "Host=localhost;Database=WebApi;Username=rob;Port=5432";
    }
}