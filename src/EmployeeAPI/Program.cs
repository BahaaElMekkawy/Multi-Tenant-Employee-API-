var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Database");

builder.Services.AddDbContext<EmployeesDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

builder.Services.AddScoped<IDataSeeder, EmployeesDataSeeder>();

var app = builder.Build();

//Migrate and seed the database on application startup
app.UseMigration();

app.Run();
