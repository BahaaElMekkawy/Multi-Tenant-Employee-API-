using EmployeeAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Database");

builder.Services.AddDbContext<EmployeesDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});


var app = builder.Build();

app.Run();
