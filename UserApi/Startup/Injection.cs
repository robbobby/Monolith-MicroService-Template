using Core.Attributes;
using UserApi.Repository;
using UserApi.Service;

namespace UserApi.Startup;

[MonolithServiceRegister]
public class Injection : IStartupInjection {
    public void Inject(IServiceCollection services) {
        UseDatabase.AddDbContext(services);

        services.AddAutoMapper(typeof(UserApiMapperProfile));

        services.AddScoped<UserService>();
        services.AddScoped<UserRepository>();
    }
}
