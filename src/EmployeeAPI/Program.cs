
var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;

//Add Services to the container.
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddCarter();

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<EmployeesDbContext>((sp, options) =>
{
    options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
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
