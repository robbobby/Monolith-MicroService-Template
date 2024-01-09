using Microsoft.Extensions.DependencyInjection;

namespace Core;

public static class UseDatabase {
    public static void AddDbContext(IServiceCollection services) {
        var connectionString = GetConnectionString();
        // services.AddDbContext<AppDbContext>(options => {
        // options.UseNpgsql(connectionString);
        // });
    }

    private static string GetConnectionString() {
        return "Host=localhost;Database=WebApi;Username=rob;Port=5432";
    }
}