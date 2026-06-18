using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using EmployeeAPI.Data;
using EmployeeAPI.Tenancy;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Testcontainers.PostgreSql;

public abstract class PostgresTestBase : IAsyncLifetime
{
    protected EmployeesDbContext DbContext;
    protected ITenantService TenantMock;

    private PostgreSqlContainer _container;

    public async Task InitializeAsync()
    {
        TenantMock = Substitute.For<ITenantService>();

        _container = new PostgreSqlBuilder()
            .WithDatabase("employee_test_db")
            .WithUsername("postgres")
            .WithPassword("postgres")
            .WithImage("postgres:16")
            .Build();

        await _container.StartAsync();

        var options = new DbContextOptionsBuilder<EmployeesDbContext>()
            .UseNpgsql(_container.GetConnectionString())
            .Options;

        DbContext = new EmployeesDbContext(options, TenantMock);

        // IMPORTANT: real migrations (not EnsureCreated)
        DbContext.Database.Migrate();
    }

    public async Task DisposeAsync()
    {
        await _container.DisposeAsync();
    }
}