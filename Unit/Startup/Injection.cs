using UnitApi.Repository;
using UnitApi.Service;
using Core.Attributes;

namespace UnitApi.Startup;

[MonolithServiceRegister]
public class Injection : IStartupInjection {
    public void Inject(IServiceCollection builderServices) {
        UseDatabase.AddDbContext(builderServices);

        builderServices.AddAutoMapper(typeof(UnitApiMapperProfile));

        builderServices.AddScoped<UnitService>();
        builderServices.AddScoped<UnitRepository>();
    }
}