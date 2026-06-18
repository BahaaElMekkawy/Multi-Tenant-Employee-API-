using EmployeeAPI.Employees.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeAPI.Data.Configurations
{
    public class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.FirstName).IsRequired().HasMaxLength(100);

            builder.Property(e => e.LastName).IsRequired().HasMaxLength(100);

            builder.Property(e => e.Email).IsRequired().HasMaxLength(100);
            builder.HasIndex(e => e.Email).IsUnique();

            builder.Property(e => e.Department).IsRequired().HasMaxLength(100);

            builder.Property(e => e.Status).IsRequired();
            
            //add index on TenantId for better performance when querying by tenant
            builder.Property(e => e.TenantId).IsRequired();
            builder.HasIndex(e => e.TenantId);

            builder.Property(e => e.CustomData).HasColumnType("jsonb");

            //mapping of the Valueobject Money to two separate columns in the Employee table
            builder.ComplexProperty(e => e.Salary, salary =>
            {
                salary.Property(m => m.AmountMinor)
                    .HasColumnName("SalaryAmountMinor")
                    .IsRequired();

                salary.Property(m => m.CurrencyCode)
                    .HasColumnName("SalaryCurrencyCode")
                    .HasMaxLength(3)
                    .IsRequired();
            });
        }
    }
}
