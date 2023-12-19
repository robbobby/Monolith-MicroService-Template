using IdentityApi.Repository;
using IdentityApi.Service;
using Core.Attributes;
using Microsoft.AspNetCore.Identity;

namespace IdentityApi.Startup;

[MonolithServiceRegister]
public class Injection : IStartupInjection {
    public void Inject(IServiceCollection builderServices) {
        UseDatabase.AddDbContext(builderServices);

        builderServices.AddAutoMapper(typeof(IdentityApiMapperProfile));

        builderServices.AddScoped<IdentityService>();
        builderServices.AddScoped<IdentityRepository>();

        builderServices.Configure<IdentityOptions>(options => {
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 8;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = true;
            
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;
            
            options.User.RequireUniqueEmail = true;
        });
    }
}