// Ordering API
using Ordering.API;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extensions;
using Ordering.Application;
var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services
        .AddApplicationServices()
        .AddInfrastructureServices(builder.Configuration)
        .AddApiServices(builder.Configuration);


var app = builder.Build();

// Http pipeline
app.UseApiServices();

if (app.Environment.IsDevelopment())
{
        await app.InitializeDatabaseAsync();
}

app.Run();
