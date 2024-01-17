using WebSocket;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapHub<OrganisationHub>("/organisationHub");

app.Run();

namespace WebSocket { }