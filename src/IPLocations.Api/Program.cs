using IPLocations.Api.Locations;

{
    var app = CreateApplication();
    ConfigureApplication(app);
    app.Run();
}

WebApplication CreateApplication()
{
    var builder = WebApplication.CreateBuilder(args);
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
    app.UseHealthChecks("/health/ready");
    app.UseHealthChecks("/health/live");
    app.MapControllers();
}

public partial class Program { }
