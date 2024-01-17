using Core.Attributes;
using MicroServiceTemplateApi.Repository;
using MicroServiceTemplateApi.Service;

namespace MicroServiceTemplateApi.Startup;

[MonolithServiceRegister]
public class Injection : IStartupInjection {
    public void Inject(IServiceCollection builderServices) {
        UseDatabase.AddDbContext(builderServices);

        builderServices.AddAutoMapper(typeof(MicroServiceTemplateApiMapperProfile));

        builderServices.AddScoped<MicroServiceTemplateService>();
        builderServices.AddScoped<MicroServiceTemplateRepository>();
    }
}
