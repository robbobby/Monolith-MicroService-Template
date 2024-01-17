using AuthServiceApi.Repository;
using AuthServiceApi.Service;
using Core.Attributes;
using Core.Entity;
using Core.Entity.Identity;
using Core.RepositoryBase;

namespace AuthServiceApi.Startup;

[MonolithServiceRegister]
public class Injection : IStartupInjection {
    public void Inject(IServiceCollection builderServices) {
        UseDatabase.AddDbContext(builderServices);
        builderServices.AddScoped<RepositoryWithEntityId<UserEntity>>();
        builderServices.AddScoped<RepositoryBase<TokenEntity>>();
        builderServices.AddScoped<IAppDbContext, AuthServiceApiDbContext>();

        builderServices.AddAutoMapper(typeof(AuthServiceApiMapperProfile));

        builderServices.AddScoped<AuthService>();
        builderServices.AddScoped<AuthServiceRepository>();
    }
}
