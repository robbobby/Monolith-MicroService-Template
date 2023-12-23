using Core.Attributes;
using UnitApi.Repository;
using UnitApi.Service;

namespace UnitApi.Startup;

[MonolithServiceRegister]
public class Injection : IStartupInjection {
    public void Inject(IServiceCollection services) {
        UseDatabase.AddDbContext(services);

        services.AddAutoMapper(typeof(UnitApiMapperProfile));

        services.AddScoped<UnitService>();
        services.AddScoped<UnitRepository>();
    }
}