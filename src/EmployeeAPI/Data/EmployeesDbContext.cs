using EmployeeAPI.Employees.Models;
using EmployeeAPI.Tenancy;
using System.Reflection;

namespace EmployeeAPI.Data
{
    public class EmployeesDbContext : DbContext
    {
        private readonly Guid _tenantId;

        public DbSet<Employee> Employees => Set<Employee>();
        public EmployeesDbContext(DbContextOptions<EmployeesDbContext> options, ITenantService tenantService) : base(options)
        {
            _tenantId = tenantService.TenantId;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<Employee>()
               .HasQueryFilter(e => e.TenantId == _tenantId && e.DeletedAt == null);

            base.OnModelCreating(modelBuilder);
        }
    }
}
