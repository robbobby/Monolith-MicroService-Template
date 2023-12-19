using Core.Attributes;
using UserApi.Repository;
using UserApi.Service;

namespace UserApi.Startup;

[MonolithServiceRegister]
public class Injection : IStartupInjection {
    public void Inject(IServiceCollection builderServices) {
        UseDatabase.AddDbContext(builderServices);
        
        builderServices.AddAutoMapper(typeof(UserApiMapperProfile));
        
        builderServices.AddScoped<UserService>();
        builderServices.AddScoped<UserRepository>();
    }
}