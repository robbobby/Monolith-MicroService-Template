using Core.Entity;
using Core.RepositoryBase;
using Microsoft.EntityFrameworkCore;

namespace ProjectServiceApi.Startup;

public class UseDatabase {
    public static void AddDbContext(IServiceCollection services) {
        var connectionString = GetConnectionString();
        services.AddDbContext<ProjectServiceApiDbContext>(options => {
            options.UseNpgsql(connectionString);
        });
    }

    private static string GetConnectionString() {
        return "Host=localhost;Database=WebApi;Username=rob;Port=5432";
    }
}

public class ProjectServiceApiDbContext(DbContextOptions<ProjectServiceApiDbContext> options)
    : DbContext(options), IAppDbContext {
    public DbSet<ProjectEntity> Projects { get; set; } = null!;
}
