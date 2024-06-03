using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data;

public static class Extensions
{
    public async static Task<IApplicationBuilder> UseMigration(this IApplicationBuilder app, ILogger logger)
    {

        try
        {
            using var scope = app.ApplicationServices.CreateScope();
            using var DbContext = scope.ServiceProvider.GetService<DiscountContext>()
                                    ?? throw new Exception("Couldn't create dbScope to run migrations");

            await DbContext.Database.MigrateAsync();  // create DB if not exists and run migrations
        }
        catch (Exception ex)
        {
            logger.LogError("Error in DB Ops {dsa}", ex.Message);
        }



        return app;
    }

}