using IdentityApi.Repository;
using Microsoft.EntityFrameworkCore;

namespace IdentityApi.Startup;

public static class UseDatabase {
    public static void AddDbContext(IServiceCollection services) {
        var connectionString = GetConnectionString();
        services.AddDbContext<IdentityApiDbContext>(options => {
            options.UseNpgsql(connectionString);
        });
    }

    private static string GetConnectionString() {
        return "Host=localhost;Database=WebApi;Username=root;Password=mysecretpassword;Port=5432";
    }
}