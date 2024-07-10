using Refit;
using Shopping.Web.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var ApiGateWay = builder.Configuration["ApiSettings:GatewayAddress"]
                ?? throw new Exception("Can't Find Base Gateway Address");

builder.Services.AddRefitClient<ICatalogService>()
                .ConfigureHttpClient(x =>
                {
                    x.BaseAddress = new Uri(ApiGateWay);
                });


builder.Services.AddRefitClient<IBasketService>()
                .ConfigureHttpClient(x =>
{
    x.BaseAddress = new Uri("https://localhost:5051");
});

builder.Services.AddRefitClient<IOrderingService>()
                .ConfigureHttpClient(x =>
{
    x.BaseAddress = new Uri(ApiGateWay);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
