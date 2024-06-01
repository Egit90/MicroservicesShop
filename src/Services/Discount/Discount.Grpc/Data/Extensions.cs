using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data;

public static class Extensions
{
    public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var DbContext = scope.ServiceProvider.GetService<DiscountContext>()
                                ?? throw new Exception("Couldn't create dbScope to run migrations");

        DbContext.Database.MigrateAsync();  // create DB if not exists and run migrations

        return app;
    }

}