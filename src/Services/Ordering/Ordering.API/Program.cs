// Ordering API
using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;
var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services
        .AddApplicationServices()
        .AddInfrastructureServices(builder.Configuration)
        .AddApiServices();


var app = builder.Build();

// Http pipeline
app.UseApiServices();

app.Run();
