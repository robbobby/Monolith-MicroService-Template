using Core.Attributes;
using Core.Entity;
using Core.RepositoryBase;
using UnitApi.Repository;
using UnitApi.Service;

namespace UnitApi.Startup;

[MonolithServiceRegister]
public class Injection : IStartupInjection {
    public void Inject(IServiceCollection services) {
        services.AddAutoMapper(typeof(UnitApiMapperProfile));

        services.AddScoped<UnitService>();
        services.AddScoped<UnitRepository>();

        services.AddScoped<RepositoryWithEntityId<OrganisationEntity>>();
        services.AddScoped<RepositoryBase<UserOrganisationEntity>>();
    }
}
