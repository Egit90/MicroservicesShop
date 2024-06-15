var builder = WebApplication.CreateBuilder(args);

// Add services


var app = builder.Build();

// Http pipeline

app.Run();
