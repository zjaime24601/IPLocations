using IPLocations.Api.Locations;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    var app = CreateApplication();
    ConfigureApplication(app);
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

WebApplication CreateApplication()
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog();
    builder.Services.AddHealthChecks();
    builder.Services.AddControllers();
    builder.Services.AddLocations();
    builder.Services.AddSwaggerGen();
    return builder.Build();
}

void ConfigureApplication(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseHealthChecks("/health/ready");
    app.UseHealthChecks("/health/live");

    app.UseSerilogRequestLogging();
    app.MapControllers();
}

public partial class Program { }
