using Carter;

var builder = WebApplication.CreateBuilder(args);

// Add Services
builder.Services.AddCarter();

var app = builder.Build();




app.MapGet("/", () => "Hello World!");

// Https pipe line

app.MapCarter();

app.Run();
