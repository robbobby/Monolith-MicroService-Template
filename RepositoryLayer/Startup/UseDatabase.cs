using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace RepositoryLayer.Startup;

public static class UseDatabase {
    public static void AddDbContext(IServiceCollection services) {
        var connectionString = GetConnectionString();
        services.AddDbContext<AppDbContext>(options => {
            options.UseNpgsql(connectionString);
        });
    }

    private static string GetConnectionString() {
        return "Host=localhost;Database=WebApi;Username=root;Password=mysecretpassword;Port=5432";
    }
}