using api.Extensions;
using business;
using data.Context;
using data.Infrastructures;
using data.Infrastructures.Repository;
using data.Seeder;
using Microsoft.EntityFrameworkCore;

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

builder.Services.AddControllers();

builder.Services.AddLogging();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApiExtension();

    // seed data
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;

    try
    {
        DatabaseSeeder.Seed(services);
    }
    catch (Exception ex)
    {
        System.Console.WriteLine("Exception: " + ex.Message);
    }
}

app.UseHttpsRedirection();
app.MapControllers();


await app.RunAsync();

