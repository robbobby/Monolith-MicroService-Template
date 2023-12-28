using AuthServiceApi.Repository;
using Microsoft.EntityFrameworkCore;

namespace AuthServiceApi.Startup;

public static class UseDatabase {
    public static void AddDbContext(IServiceCollection services) {
        var connectionString = GetConnectionString();
        services.AddDbContext<AuthServiceApiDbContext>(options => {
            options.UseNpgsql(connectionString);
        });
    }

    private static string GetConnectionString() {
        return "Host=localhost;Database=WebApi;Username=root;Password=mysecretpassword;Port=5432";
    }
}