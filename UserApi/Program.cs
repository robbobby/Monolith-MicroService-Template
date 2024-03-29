using UserApi.Startup;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

new Injection().Inject(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

new ServiceStartup().Configure(app);

app.Run();

namespace UserApi {
    internal record WeatherForecast(DateOnly Date,
                                    int TemperatureC,
                                    string? Summary) {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}
