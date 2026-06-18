using EmployeeAPI.Employees.Models;
using EmployeeAPI.Shared.ValueObjects;

namespace EmployeeAPI.Data.Seed
{
    public static class InitialData
    {
        //intial tenents (tenantA - teenantB)
        public static Guid TenantAId = Guid.Parse("c5547731-4c2b-4a12-8530-72b88f045956");
        public static Guid TenantBId = Guid.Parse("090697e6-7112-453f-88fe-740bcaad37bb");

        public static List<Employee> Employees => new()
        {
            new Employee
            {
                Id = new Guid("9f1b6a4e-3c2d-4b8a-9c7f-1e5a2d6f8b90"),
                TenantId = TenantAId,
                FirstName = "Ali",
                LastName = "Hassan",
                Email = "ali@tenanta.com",
                Department = "IT",
                Status = EmployeeStatus.Active,
                Salary = Money.Of(600, "USD"),
                CreatedAt = DateTime.UtcNow
            },
            new Employee
            {
                Id = new Guid("a1c3d5e7-1111-4a2b-8c9d-1234567890ab"),
                TenantId = TenantAId,
                FirstName = "Sara",
                LastName = "Ahmed",
                Email = "sara@tenanta.com",
                Department = "HR",
                Status = EmployeeStatus.Active,
                Salary = Money.Of(600, "USD"),
                CreatedAt = DateTime.UtcNow
            },
            new Employee
            {
                Id = new Guid("b2d4f6a8-2222-4c3d-9e0f-abcdefabcdef"),
                TenantId = TenantAId,
                FirstName = "Omar",
                LastName = "Ali",
                Email = "omar@tenanta.com",
                Department = "Finance",
                Status = EmployeeStatus.Suspended,
                Salary = Money.Of(5000, "EGP"),
                CreatedAt = DateTime.UtcNow
            },

            new Employee
            {
                Id = new Guid("c3e5a7b9-3333-4d4e-a1b2-fedcba987654"),
                TenantId = TenantBId,
                FirstName = "Mona",
                LastName = "Youssef",
                Email = "mona@tenantb.com",
                Department = "IT",
                Status = EmployeeStatus.Active,
                Salary = Money.Of(600, "USD"),
                CreatedAt = DateTime.UtcNow
            },
            new Employee
            {
                Id = new Guid("d4f6b8c0-4444-4e5f-b3c4-0123456789cd"),
                TenantId = TenantBId,
                FirstName = "Khaled",
                LastName = "Nabil",
                Email = "khaled@tenantb.com",
                Department = "Sales",
                Status = EmployeeStatus.Active,
                Salary = Money.Of(5000, "EGP"),
                CreatedAt = DateTime.UtcNow
            }
        };
    }
}