using Core.Attributes;

namespace UserApi.Startup;

[MonolithStartupRegister]
public class ServiceStartup : IServiceStartup {
    public void Configure(WebApplication app) {
        app.MapControllers();
    }
}
