using Microsoft.Extensions.DependencyInjection;

namespace ServiceLayer;

public class Injection {
    public static void Inject(IServiceCollection builderServices) {
        builderServices.AddScoped<UnitService>();
    }
}