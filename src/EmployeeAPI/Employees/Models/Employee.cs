using EmployeeAPI.Shared.ValueObjects;
using Shared.AuditableEntity;
using Shared.TenantProvider;
using System.Text.Json;

namespace EmployeeAPI.Employees.Models
{
    public class Employee : Entity<Guid>, ITenantProvider
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public EmployeeStatus Status { get; set; }
        public JsonDocument? CustomData { get; set; }
        public Guid TenantId { get; set; }
        public Money Salary { get; set; } = default!;
    }
}
