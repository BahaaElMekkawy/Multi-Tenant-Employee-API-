using EmployeeAPI.Data;
using Microsoft.EntityFrameworkCore;
using Shared.Seed;

namespace EmployeeAPI.Extensions
{
    public static class Extensions
    {
        public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<EmployeesDbContext>();

            // Apply migrations
            dbContext.Database.Migrate();

            // Seed data
            var seeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();
            seeder.SeedAllAsync().GetAwaiter().GetResult();

            return app;
        }
    }
}