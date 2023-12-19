using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using RepositoryLayer.Startup;

namespace RepositoryLayer;

[UsedImplicitly]
public class Injection {
    public static void Inject(IServiceCollection builderServices) {
        UseDatabase.AddDbContext(builderServices);
    }
}