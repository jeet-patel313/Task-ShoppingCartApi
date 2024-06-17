using Microsoft.EntityFrameworkCore;
using ShoppingCartApi.Contexts;
using ShoppingCartApi.Repositories.Implementation;
using ShoppingCartApi.Repositories.Interfaces;
using ShoppingCartApi.Services.Implementations;
using ShoppingCartApi.Services.Interfaces;
using AutoMapper;
using ShoppingCartApi.Infrastructure.Mappers;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables from .env file
Env.Load();

// Get the connection string from the environment variables
var connectionString = Environment.GetEnvironmentVariable("DEFAULT_CONNECTION");

// Ensure the connection string is not null or empty
if (string.IsNullOrEmpty(connectionString))
{
  throw new InvalidOperationException("Connection string 'DEFAULT_CONNECTION' is not set in the environment variables.");
}

// Add services to the container
builder.Services.AddControllers();

// Configure Entity Framework to use SQL Server with the provided connectionString
builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(connectionString));

// Add AutoMapper with the specified profile
builder.Services.AddAutoMapper(typeof(CartProfile));

// Register the repository and service dependencies
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartService, CartService>();

// Add support for API documentation and testing
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// // Configure the HTTP request pipeline
// if (app.Environment.IsDevelopment())
// {
//   // Use Swagger in development environment for API documentation
//   app.UseSwagger();
//   app.UseSwaggerUI();
// }

// Use Swagger in both development and production environments for API documentation
app.UseSwagger();
app.UseSwaggerUI(c =>
{
  c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShoppingCart API V1 - Jeet");
  c.RoutePrefix = string.Empty;
});

// Enable HTTPS redirection
app.UseHttpsRedirection();

// Enable authorization middleware
app.UseAuthorization();

// Map controller routes
app.MapControllers();

// Run the application
app.Run();
