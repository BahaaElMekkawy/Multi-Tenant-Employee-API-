using Microsoft.EntityFrameworkCore;
using Shared.Seed;

namespace EmployeeAPI.Data.Seed
{
    public class EmployeesDataSeeder(EmployeesDbContext dbContext) : IDataSeeder
    {
        public async Task SeedAllAsync()
        {
            if (!await dbContext.Employees.AnyAsync())
            {
                await dbContext.Employees.AddRangeAsync(InitialData.Employees);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
