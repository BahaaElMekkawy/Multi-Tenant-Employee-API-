using EmployeeAPI.Employees.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EmployeeAPI.Data
{
    public class EmployeesDbContext : DbContext
    {
        public DbSet<Employee> Employees => Set<Employee>();
        public EmployeesDbContext(DbContextOptions<EmployeesDbContext> options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
