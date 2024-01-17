using Core.Attributes;

namespace AuthServiceApi.Startup;

[MonolithStartupRegister]
public class ServiceStartup : IServiceStartup {
    public void Configure(WebApplication app) {
        app.MapControllers();
    }
}
