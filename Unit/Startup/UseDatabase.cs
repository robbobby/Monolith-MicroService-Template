using Microsoft.EntityFrameworkCore;
using UnitApi.Repository;

namespace UnitApi.Startup;

public static class UseDatabase {
    public static void AddDbContext(IServiceCollection services) {
        var connectionString = GetConnectionString();
        services.AddDbContext<UnitApiDbContext>(options => {
            options.UseNpgsql(connectionString);
        });
    }

    private static string GetConnectionString() {
        return "Host=localhost;Database=WebApi;Username=root;Password=mysecretpassword;Port=5432";
    }
}