using Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace UnitApi.Repository;

public class UnitApiDbContext(DbContextOptions<UnitApiDbContext> options) : DbContext(options) {
    public DbSet<UnitEntity> Units { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes()) {
            if (entityType.ClrType.Name.EndsWith("Entity")) {
                modelBuilder.Entity(entityType.ClrType).ToTable(entityType.ClrType.Name.Replace("Entity", ""));
            }
        }
    }
}