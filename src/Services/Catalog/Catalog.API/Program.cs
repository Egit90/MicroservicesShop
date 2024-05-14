using Carter;
using Marten;

var builder = WebApplication.CreateBuilder(args);


// Add services to container
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
});

builder.Services.AddMarten(opts =>
{
    var MartinConnectionString = builder.Configuration.GetConnectionString("DataBase")
                                ?? throw new InvalidOperationException("Connection string Not Found");

    opts.Connection(MartinConnectionString);
}).UseLightweightSessions();

var app = builder.Build();

// http pipeline
app.MapCarter();

app.Run();

