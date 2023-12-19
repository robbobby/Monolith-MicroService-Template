using Core.Attributes;
using Microsoft.AspNetCore.Identity;

namespace IdentityApi.Startup;

[MonolithStartupRegister]
public class ServiceStartup : IServiceStartup {
    public void Configure(WebApplication app) {
        app.MapControllers();
        
        app.MapGroup("/api/identity").MapIdentityApi<IdentityUser>();
    }
}