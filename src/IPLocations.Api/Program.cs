using IPLocations.Api.Locations;

var builder = WebApplication.CreateBuilder(args);

// TODO Introduce startup error handling

// Configure
builder.Services.AddControllers();
builder.Services.AddLocations(builder.Configuration);


var app = builder.Build();

// Middleware
app.MapControllers();


app.Run();

public partial class Program { }
