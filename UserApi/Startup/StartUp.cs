using Core.Attributes;

namespace UserApi;

[MonolithStartupRegister]
public class ServiceStartup : IServiceStartup {
    public void Configure(WebApplication app) {
        app.MapControllers();
    }
}