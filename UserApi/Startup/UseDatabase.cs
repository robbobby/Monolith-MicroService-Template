using Core;
using Microsoft.EntityFrameworkCore;
using UserApi.Repository;

namespace UserApi.Startup;

public static class UseDatabase {
    public static void AddDbContext(IServiceCollection services) {
        var connectionString = GetConnectionString();

        services.AddDbContext<IUserDbContext, UserApiDbContext>(options => {
            options.UseNpgsql(connectionString);
        });
    }

    private static string GetConnectionString() {
        return "Host=localhost;Database=WebApi;Username=rob;Port=5432";
    }
}
