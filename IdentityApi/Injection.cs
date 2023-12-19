using Core.Attributes;

namespace IdentityApi;

[MonolithServiceRegister]
public class Injection : IStartupInjection {
    public void Inject(IServiceCollection builderServices) {
    }
}