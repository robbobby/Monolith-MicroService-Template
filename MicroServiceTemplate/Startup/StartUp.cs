using Core.Attributes;

namespace MicroServiceTemplateApi.Startup;

[MonolithStartupRegister]
public class ServiceStartup : IServiceStartup {
    public void Configure(WebApplication app) {
        app.MapControllers();
    }
}