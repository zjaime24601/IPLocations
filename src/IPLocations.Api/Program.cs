var builder = WebApplication.CreateBuilder(args);

// TODO Introduce startup error handling

// Configure
builder.Services.AddControllers();


var app = builder.Build();

// Middleware
app.MapControllers();


app.Run();

public partial class Program { }