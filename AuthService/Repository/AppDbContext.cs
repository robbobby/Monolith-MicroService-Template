using Core.Entity.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthServiceApi.Repository;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) {
    public DbSet<UserEntity> AuthServices { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder) { }
}