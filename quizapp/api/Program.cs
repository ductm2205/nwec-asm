using api.Extensions;
using business;
using business.Services.Auth;
using data.Context;
using data.Infrastructures;
using data.Infrastructures.Repository;
using data.Seeder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using models.Auth;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApiExtension();

// Register DbContext
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

// Register UnitOfWork
builder.Services.AddScoped(typeof(IBaseItemRepo<>), typeof(BaseItemRepo<>));
builder.Services.AddScoped(typeof(IBaseEntityRepo<>), typeof(BaseEntityRepo<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Register mediater
builder.Services.AddMediatR(
    config => config.RegisterServicesFromAssembly(typeof(Business).Assembly)
);

builder.Services.AddControllers().AddJsonOptions(
    opt =>
    {
        opt.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault;
    }
);

builder.Services.AddLogging();

// Register Identity
builder.Services.AddIdentity<User, Role>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Register token service
builder.Services.AddScoped<ITokenService, TokenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApiExtension();

    // seed data
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

    var userPath = Path.Combine(app.Environment.WebRootPath, "data", "users.json");
    var rolePath = Path.Combine(app.Environment.WebRootPath, "data", "roles.json");

    try
    {
        DatabaseSeeder.Seed(services);
        UserSeeder.Seed(services, userManager, roleManager, userPath, rolePath);
    }
    catch (Exception ex)
    {
        System.Console.WriteLine("Exception: " + ex.Message);
    }
}

app.UseHttpsRedirection();
app.MapControllers();


await app.RunAsync();

