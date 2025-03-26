
using Infrastructure.Persistence;
using API.Configuration;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var dbConfig = new DBConnections();

builder.Services.AddDbContext<ProjectDBContext>(options =>
    options.UseSqlServer(dbConfig.ConnectionString));

// Registro de repositorios y servicios

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

