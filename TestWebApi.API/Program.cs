using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using TestWebApi.Infrastructure.Data;
using TestWebApi.Infrastructure.Repositories;
using TestWebApi.Application.Interfaces;
using TestWebApi.Application.Mapping;
using AutoMapper;
using TestWebApi.Infrastructure.Db;
using Microsoft.EntityFrameworkCore.Internal;
var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TestWebApi API", Version = "v1" });
});

// DbContext
builder.Services.AddDbContext<MainDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("SecondConnection"),
        npgsqlOptions =>
        {
            npgsqlOptions.MigrationsHistoryTable("__ef_migrations_history"); // keeps PascalCase
        }
    )
    .UseSnakeCaseNamingConvention() // everything else still snake_case
);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        npgsqlOptions =>
        {
            npgsqlOptions.MigrationsHistoryTable("__ef_migrations_history"); // keeps PascalCase
        }
    )
    .UseSnakeCaseNamingConvention() // everything else still snake_case
);
// DI for repository
builder.Services.AddSingleton<IDbContextFactory, RestoAppDbContextFactory>();
builder.Services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
builder.Services.AddScoped<IRestoRepository, RestoRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddAutoMapper(typeof(CustomerProfile));
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IBranchRepository,BranchRepository>();
var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestWebApi API V1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();