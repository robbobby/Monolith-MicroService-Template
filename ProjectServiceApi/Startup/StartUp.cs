using Core.Attributes;

namespace ProjectServiceApi.Startup;

[MonolithStartupRegister]
public class ServiceStartup : IServiceStartup {
    public void Configure(WebApplication app) {
        app.MapControllers();
    }
}
