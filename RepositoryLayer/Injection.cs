using Microsoft.Extensions.DependencyInjection;
using RepositoryLayer.Repository;
using RepositoryLayer.Startup;

namespace RepositoryLayer;

public class Injection {
    public static void Inject(IServiceCollection builderServices) {
        UseDatabase.AddDbContext(builderServices);

        builderServices.AddScoped<UnitRepository>();
    }
}