using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Infrastructure.Data;

namespace Ordering.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DataBase")
                                ?? throw new Exception("Ordering api is missing the connection string");

        services.AddDbContext<ApplicationDbContext>(opts =>
        {
            opts.UseSqlServer(connectionString);
        });

        return services;
    }

}
