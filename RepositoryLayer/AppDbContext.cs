using Core;
using Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace RepositoryLayer;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) {
    public DbSet<Unit> Units { get; set; }
    public DbSet<User> Users { get; set; }

    public DbSet<UserUnit> UserUnits { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder) { }
}