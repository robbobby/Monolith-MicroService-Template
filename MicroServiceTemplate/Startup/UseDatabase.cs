using MicroServiceTemplateApi.Repository;
using Microsoft.EntityFrameworkCore;

namespace MicroServiceTemplateApi.Startup;

public static class UseDatabase {
    public static void AddDbContext(IServiceCollection services) {
        var connectionString = GetConnectionString();
        services.AddDbContext<MicroServiceTemplateApiDbContext>(options => {
            options.UseNpgsql(connectionString);
        });
    }

    private static string GetConnectionString() {
        return "Host=localhost;Database=WebApi;Username=rob;Port=5432";
    }
}