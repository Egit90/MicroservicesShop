using BuildingBlocks.Behaviors;
using Carter;

var builder = WebApplication.CreateBuilder(args);

// Add Services
builder.Services.AddCarter();

builder.Services.AddMediatR(c =>
{
    c.RegisterServicesFromAssembly(typeof(Program).Assembly);
    c.AddOpenBehavior(typeof(ValidationBehavior<,>));
    c.AddOpenBehavior(typeof(LoggingBehavior<,>));
});



var app = builder.Build();




app.MapGet("/", () => "Hello World!");

// Https pipe line

app.MapCarter();

app.Run();
