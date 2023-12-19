namespace MonoApp.Startup;

public abstract class SwaggerStartup {
    public static void App(WebApplication app) {
        if (!app.Environment.IsDevelopment())
            return;
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    public static void AddSwagger(IServiceCollection services) {
        services.AddSwaggerGen();
    }
}