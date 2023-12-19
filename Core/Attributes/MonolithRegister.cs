using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Attributes;

[MeansImplicitUse]
[BaseTypeRequired(typeof(IStartupInjection))] 
public class MonolithServiceRegisterAttribute : Attribute;

[MeansImplicitUse]
[BaseTypeRequired(typeof(IServiceStartup))]
public class MonolithStartupRegisterAttribute : Attribute;

public interface IStartupInjection {
    void Inject(IServiceCollection builderServices);
}

public interface IServiceStartup {
    void Configure(WebApplication app);
}