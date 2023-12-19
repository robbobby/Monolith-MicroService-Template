using Microsoft.EntityFrameworkCore;

namespace MicroServiceTemplateApi.Repository;

public class MicroServiceTemplateApiDbContext(DbContextOptions<MicroServiceTemplateApiDbContext> options) : DbContext(options) {
    public DbSet<MicroServiceTemplateEntity> MicroServiceTemplates { get; set; }
}