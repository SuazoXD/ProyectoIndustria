using Application.Interfaces;
using Application.Services;
using Infrastructure.Repositories;
using Infrastructure.Persistence;
using API.Configuration;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var dbConfig = new DBConnections();

builder.Services.AddDbContext<ProjectDBContext>(options =>
    options.UseSqlServer(dbConfig.ConnectionString));

// Registro de repositorios y servicios
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

