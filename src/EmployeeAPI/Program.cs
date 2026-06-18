
using EmployeeAPI.Tenancy;
using Shared.Exceptions.Handler;

var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;

//Add Services to the container.
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    //config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    //config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddCarter();

var connectionString = builder.Configuration.GetConnectionString("Database");

builder.Services.AddDbContext<EmployeesDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

builder.Services.AddScoped<IDataSeeder, EmployeesDataSeeder>();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ITenantService, TenantService>();


var app = builder.Build();

//configure carter for endpoints
app.MapCarter();

// the empty options to use our custom exception handler
app.UseExceptionHandler(options => { }); 

//Migrate and seed the database on application startup
app.UseMigration();

app.Run();
