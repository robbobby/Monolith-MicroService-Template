
using Core.Attributes;

namespace WebSocket;

[MonolithStartupRegister]
public class ServiceStartup : IServiceStartup {
    public void Configure(WebApplication app) {
        app.MapHub<OrganisationHub>("/organisationHub");
    }
}