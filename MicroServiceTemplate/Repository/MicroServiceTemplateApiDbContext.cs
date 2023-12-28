using Core.RepositoryBase;
using Microsoft.EntityFrameworkCore;

namespace MicroServiceTemplateApi.Repository;

public class MicroServiceTemplateApiDbContext(DbContextOptions<MicroServiceTemplateApiDbContext> options)
    : DbContext(options), IAppDbContext {
    public DbSet<MicroServiceTemplateEntity> MicroServiceTemplates { get; set; }
}