using Microsoft.EntityFrameworkCore;

namespace MicroServiceTemplateApi.Repository;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) {
    public DbSet<MicroServiceTemplateEntity> MicroServiceTemplates { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder) { }
}