using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Data.Interceptors;

namespace Ordering.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DataBase")
                                ?? throw new Exception("Ordering api is missing the connection string");

        services.AddDbContext<ApplicationDbContext>(opts =>
        {
            opts.AddInterceptors(new AuditableEntityInterceptor());
            opts.UseSqlServer(connectionString);
        });

        return services;
    }

}
