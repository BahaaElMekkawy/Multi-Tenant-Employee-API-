using EmployeeAPI.Employees.Dtos;
using EmployeeAPI.Employees.Features.AddEmployee;
using EmployeeAPI.Employees.Features.GetEmployees;
using EmployeeAPI.Employees.Models;
using EmployeeAPI.Shared.ValueObjects;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Shared.Pagination;

namespace EmployeeAPI.Tests.Employees;

public class EmployeeSmokeTests : PostgresTestBase
{
    private readonly Guid _tenantA = Guid.NewGuid();
    private readonly Guid _tenantB = Guid.NewGuid();

    // CREATE EMPLOYEE TEST
    [Fact]
    public async Task CreateEmployee_Should_Succeed()
    {
        // Arrange
        TenantMock.TenantId.Returns(_tenantA);

        var handler = new CreateEmployeeCommandHandler(DbContext, TenantMock);

        var dto = new CreateEmployeeDto(
            "Ali",
            "Hassan",
            "ali@test.com",
            "IT",
            "Active",
            null,
            new MoneyDto { AmountMinor = 100000, CurrencyCode = "EGP" }
        );

        // Act
        var result = await handler.Handle(
            new CreateEmployeeCommand(dto),
            CancellationToken.None);

        // Assert
        result.Id.Should().NotBeEmpty();

        var saved = DbContext.Employees.IgnoreQueryFilters().FirstOrDefault(emp => emp.Id == result.Id && emp.TenantId == _tenantA);
        saved.Should().NotBeNull();
        saved.Email.Should().Be("ali@test.com");
        saved.TenantId.Should().Be(_tenantA);
    }

    // DUPLICATE EMAIL TEST
    [Fact]
    public async Task Should_Not_Allow_Duplicate_Email()
    {
        TenantMock.TenantId.Returns(_tenantA);

        DbContext.Employees.Add(new Employee
        {
            Id = Guid.NewGuid(),
            TenantId = _tenantA,
            Email = "dup@test.com",
            FirstName = "A",
            LastName = "B",
            Department = "IT",
            Status = EmployeeStatus.Active,
            Salary = Money.Of(100000, "EGP")

        });

        await DbContext.SaveChangesAsync();

        var handler = new CreateEmployeeCommandHandler(DbContext, TenantMock);

        var dto = new CreateEmployeeDto(
            "Ali",
            "Hassan",
            "dup@test.com",
            "IT",
            "Active",
            null,
            new MoneyDto { AmountMinor = 100000, CurrencyCode = "EGP" }
        );

        Func<Task> act = async () =>
            await handler.Handle(new CreateEmployeeCommand(dto), CancellationToken.None);

        await act.Should().ThrowAsync<Exception>();
    }

    // TENANT ISOLATION TESt
    [Fact]
    public async Task Should_Isolate_Tenants_Correctly()
    {
        // Arrange - tenant A data
        DbContext.Employees.Add(new Employee
        {
            Id = Guid.NewGuid(),
            TenantId = _tenantA,
            Email = "a@test.com",
            FirstName = "A",
            LastName = "A",
            Department = "IT",
            Status = EmployeeStatus.Active,
            Salary = Money.Of(100000, "EGP")
        });

        await DbContext.SaveChangesAsync();

        // Act - switch tenant
        TenantMock.TenantId.Returns(_tenantB);

        var handler = new GetEmployeesHandler(DbContext);

        var result = await handler.Handle(
            new GetEmployeesQuery(new PaginationRequest(1, 10)),
            CancellationToken.None);

        // Assert
        result.employees.Data.Should().BeEmpty();
    }
}