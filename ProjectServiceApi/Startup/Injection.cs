using Core.Attributes;
using Core.Entity.Project;
using Core.RepositoryBase;
using ProjectServiceApi.Repository;
using ProjectServiceApi.Services;

namespace ProjectServiceApi.Startup;

[MonolithServiceRegister]
public class Injection : IStartupInjection {
    public void Inject(IServiceCollection services) {
        UseDatabase.AddDbContext(services);
        services.AddAutoMapper(typeof(ProjectApiMapperProfile));

        services.AddScoped<ProjectService>();
        services.AddScoped<ProjectRepository>();

        services.AddScoped<TicketService>();
        services.AddScoped<TicketRepository>();

        services.AddScoped<RepositoryWithEntityId<ProjectEntity>>();
        services.AddScoped<RepositoryWithEntityId<TicketEntity>>();
    }
}
