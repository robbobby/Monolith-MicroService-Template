using Core.Attributes;
using IStartup = Microsoft.AspNetCore.Hosting.IStartup;

namespace IdentityApi;

[MonolithStartupRegister]
public class ServiceStartup : IServiceStartup {
    private static readonly string[] Summaries = new[] {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public void Configure(WebApplication app) {
        app.MapGet("/weatherforecast", () => {
                var forecast = Enumerable.Range(1, 5).Select(index =>
                        new WeatherForecast
                        (
                            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                            Random.Shared.Next(-20, 55),
                            Summaries[Random.Shared.Next(Summaries.Length)]
                        ))
                    .ToArray();
                return forecast;
            })
            .WithName("GetWeatherForecast")
            .WithOpenApi();
    }
}