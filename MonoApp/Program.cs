using System.Security.Authentication;
using Core;
using Core.Attributes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.Authority = builder.Configuration["Identity:Authority"];
        options.Audience = builder.Configuration["Identity:Audience"];

        options.TokenValidationParameters = new TokenValidationParameters {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true
        };
    });

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) => {
    options.UseNpgsql("Host=localhost;Database=WebApi;Username=rob;Port=5432", dbOptions => {
        dbOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.GetName().Name);
    });
});

builder.Services.AddAuthorization();

builder.Services.AddCors(options => {
    options.AddDefaultPolicy(opts => {
        opts.WithOrigins("https://localhost:5001") // Replace with the origin of your client application
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<AppDbContext>(opts =>
    opts.UseNpgsql("Host=localhost;Database=WebApi;Username=rob;Port=5432"));

InjectMicroServices();


var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

var httpsOptions = new HttpsConnectionAdapterOptions {
    SslProtocols = SslProtocols.Tls12
};

MapMicroServices();

app.MapControllers();
app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment()) {
    using var scope = app.Services.CreateScope();

    await scope.ServiceProvider.GetRequiredService<AppDbContext>().Database.MigrateAsync();
}


app.Run();
return;

void MapMicroServices() {
    foreach (var service in GetMonolithServices(typeof(MonolithStartupRegisterAttribute)))
        if (Activator.CreateInstance(service) is IServiceStartup startUp)
            startUp.Configure(app);
        else
            throw new Exception(
                $"Error building monolith: Service {service.Name} from assembly {service.Assembly.GetName().Name} does not implement {nameof(IServiceStartup)}");
}

void InjectMicroServices() {
    foreach (var service in GetMonolithServices(typeof(MonolithServiceRegisterAttribute)))
        if (Activator.CreateInstance(service) is IStartupInjection startupInjection)
            startupInjection.Inject(builder.Services);
        else
            throw new Exception(
                $"Error configuring monolith: Service {service.Name} from assembly {service.Assembly.GetName().Name} does not implement {nameof(IStartupInjection)}");
}

IEnumerable<Type> GetMonolithServices(Type attributeType) {
    foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
    foreach (var type in assembly.GetTypes())
        if (type.GetCustomAttributes(attributeType, true).Length > 0)
            yield return type;
}

void ResolveDbContextOptions(IServiceProvider provider, DbContextOptionsBuilder builder) {
    builder.UseNpgsql("Host=localhost;Database=WebApi;Username=rob;Port=5432",
        NpsqlOptionsAction);
}

void NpsqlOptionsAction(NpgsqlDbContextOptionsBuilder builder) {
    builder.MigrationsAssembly(typeof(Program).Assembly.GetName().Name);
}