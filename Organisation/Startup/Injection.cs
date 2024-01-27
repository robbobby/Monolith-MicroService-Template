using Core.Attributes;
using Core.Entity.Identity;
using Core.RepositoryBase;
using OrganisationApi.Repository;
using OrganisationApi.Service;

namespace OrganisationApi.Startup;

[MonolithServiceRegister]
public class Injection : IStartupInjection {
    public void Inject(IServiceCollection services) {
        services.AddAutoMapper(typeof(OrganisationApiMapperProfile));

        services.AddScoped<OrganisationService>();
        services.AddScoped<OrganisationRepository>();

        services.AddScoped<RepositoryWithEntityId<OrganisationEntity>>();
        services.AddScoped<RepositoryBase<UserOrganisationEntity>>();
    }
}
