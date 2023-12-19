using Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace IdentityApi.Repository;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) {
    public DbSet<IdentityEntity> Identitys { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder) { }
}