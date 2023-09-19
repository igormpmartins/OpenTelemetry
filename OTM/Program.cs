using OTM;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Trace;
using OpenTelemetry.Resources;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IUserProvider>(new UserProvider("Server=(LocalDb)\\MSSQLLocalDB;Database=randomdb;Trusted_Connection=True;"));

var serviceName = "StreetController";

builder.Services.AddOpenTelemetry().WithTracing(o =>
{
    o.AddConsoleExporter()
        .AddSource(serviceName)
        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName))
        .AddAspNetCoreInstrumentation()
        .AddSqlClientInstrumentation()
        .AddHttpClientInstrumentation();
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
