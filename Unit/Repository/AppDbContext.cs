using Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace UnitApi.Repository;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) {
    public DbSet<OrganisationEntity> Units { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder) { }
}