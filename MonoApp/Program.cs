using Core;
using Core.Attributes;
using IdentityApi.Repository;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

InjectMicroServices();

RepositoryLayer.Injection.Inject(builder.Services);

builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MapperProfile));

builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<IdentityApiDbContext>();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

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
            throw new Exception(
                $"Error building monolith: Service {service.Name} from assembly {service.Assembly.GetName().Name} does not implement {nameof(IServiceStartup)}");
        }
    }
}

void InjectMicroServices() {
    foreach (var service in GetMonolithServices(typeof(MonolithServiceRegisterAttribute))) {
        if (Activator.CreateInstance(service) is IStartupInjection startupInjection) {
            startupInjection.Inject(builder.Services);
        } else {
            throw new Exception(
                $"Error configuring monolith: Service {service.Name} from assembly {service.Assembly.GetName().Name} does not implement {nameof(IStartupInjection)}");
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