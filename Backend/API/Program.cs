
using Infrastructure.Persistence;
using API.Configuration;
using Microsoft.EntityFrameworkCore;
using Aplication.Interfaces.Roles;
using Aplication.Services.Roles;
using Infrastructure.Repositories.Roles;
using Aplication.Interfaces.Usuarios;
using Aplication.Services.Usuarios;
using Infrastructure.Repositories.Usuarios;
using Aplication.Interfaces.Permisos;
using Application.Services.Permisos;
using Infrastructure.Repositories.Permisos;
using Aplication.Interfaces.Privacidades;
using Aplication.Services.Privacidades;
using Infrastructure.Repositories.Privacidades;

var builder = WebApplication.CreateBuilder(args);

var dbConfig = new DBConnections();

builder.Services.AddDbContext<ProjectDBContext>(options =>
    options.UseSqlServer(dbConfig.ConnectionString));

builder.Services.AddScoped<IRolRepository, RolRepository>();
builder.Services.AddScoped<IRolService, RolService>();

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddScoped<IPermisoRepository, PermisoRepository>();
builder.Services.AddScoped<IPermisoService, PermisoService>();

builder.Services.AddScoped<IPrivacidadRepository, PrivacidadRepository>();
builder.Services.AddScoped<IPrivacidadService, PrivacidadService>();

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

