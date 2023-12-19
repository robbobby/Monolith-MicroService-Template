using Core;
using Core.Attributes;
using MonoApp.Startup;
using RepositoryLayer.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

InjectMicroServices();

RepositoryLayer.Injection.Inject(builder.Services);
ServiceLayer.Injection.Inject(builder.Services);

SwaggerStartup.AddSwagger(builder.Services);

builder.Services.AddAutoMapper(typeof(MapperProfile));

var app = builder.Build();

SwaggerStartup.App(app);

MapMicroServices();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
return;

void MapMicroServices() {
    foreach (var service in GetMonolithServices(typeof(MonolithStartupRegisterAttribute))) {
        if (Activator.CreateInstance(service) is IServiceStartup startUp) {
            startUp.Configure(app);
        } else {
            throw new Exception($"Error building monolith: Service {service.Name} from assembly {service.Assembly.GetName().Name} does not implement {nameof(IServiceStartup)}");
        }
    }
}

void InjectMicroServices() {
    foreach (var service in GetMonolithServices(typeof(MonolithServiceRegisterAttribute))) {
        if (Activator.CreateInstance(service) is IStartupInjection startupInjection) {
            startupInjection.Inject(builder.Services);
        } else {
            throw new Exception($"Error configuring monolith: Service {service.Name} from assembly {service.Assembly.GetName().Name} does not implement {nameof(IStartupInjection)}");
        }
    }
}

IEnumerable<Type> GetMonolithServices(Type attributeType) {
    foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies()) {
        foreach (var type in assembly.GetTypes()) {
            if (type.GetCustomAttributes(attributeType, true).Length > 0) {
                yield return type;
            }
        }
    }
}