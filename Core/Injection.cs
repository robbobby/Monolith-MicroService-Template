using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace Core;

[UsedImplicitly]
public class Injection {
    public static void Inject(IServiceCollection builderServices) {
        UseDatabase.AddDbContext(builderServices);
    }
}