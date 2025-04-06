
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
using Microsoft.Data.SqlClient;
using API.Custom;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using API;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Aplication.Interfaces.Archivos;
using Aplication.Interfaces.ArchivosListaReproduccion;
using Aplication.Interfaces.ContenidosPremium;
using Aplication.Interfaces.Creditos;
using Aplication.Interfaces.Facturas;
using Aplication.Interfaces.Favoritos;
using Aplication.Interfaces.ListasDeReproduccion;
using Aplication.Interfaces.MetodosPago;
using Aplication.Interfaces.Pagos;
using Aplication.Services.Archivos;
using Aplication.Services.ArchivosListaReproduccion;
using Aplication.Services.ContenidosPremium;
using Aplication.Services.Creditos;
using Aplication.Services.Facturas;
using Aplication.Services.Favoritos;
using Aplication.Services.ListasDeReproduccion;
using Aplication.Services.Pagos;
using Application.Services.MetodosPago;
using Infrastructure.Repositories.Archivos;
using Infrastructure.Repositories.ContenidosPremium;
using Infrastructure.Repositories.Creditos;
using Infrastructure.Repositories.Facturas;
using Infrastructure.Repositories.Favoritos;
using Infrastructure.Repositories.ListasDeReproduccion;
using Infrastructure.Repositories.MetodosPago;
using Infrastructure.Repositories.Pagos;
using Infrastructure.Repositories;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configurar JWT
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = jwtSettings.GetValue<string>("Key");
var issuer = jwtSettings.GetValue<string>("Issuer");
var audience = jwtSettings.GetValue<string>("Audience");


var dbConfig = new DBConnections();

builder.Services.AddDbContext<ProjectDBContext>(options =>
    options.UseSqlServer(dbConfig.ConnectionString));



//Utilidades
builder.Services.AddScoped<Utilidades>();
builder.Services.AddAuthentication(configureOptions =>
{
    configureOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    configureOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(configureOptions =>
{
    configureOptions.RequireHttpsMetadata = false;
    configureOptions.SaveToken = true;
    configureOptions.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

// Configurar CORS  
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Registrar las dependencias necesarias (repositorios y servicios)
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddScoped<IArchivoRepository, ArchivoRepository>();
builder.Services.AddScoped<IArchivoService, ArchivoService>();

builder.Services.AddScoped<IRolRepository, RolRepository>();
builder.Services.AddScoped<IRolService, RolService>();

builder.Services.AddScoped<IMetodoPagoRepository, MetodoPagoRepository>();
builder.Services.AddScoped<IMetodoPagoService, MetodoPagoService>();

builder.Services.AddScoped<IArchivoListaReproduccionRepository, ArchivoListaReproduccionRepository>();
builder.Services.AddScoped<IArchivoListaReproduccionService, ArchivoListaReproduccionService>();

builder.Services.AddScoped<IPermisoRepository, PermisoRepository>();
builder.Services.AddScoped<IPermisoService, PermisoService>();

builder.Services.AddScoped<IPagoRepository, PagoRepository>();
builder.Services.AddScoped<IPagoService, PagoService>();

builder.Services.AddScoped<IFacturaRepository, FacturaRepository>();
builder.Services.AddScoped<IFacturaService, FacturaService>();

builder.Services.AddScoped<ICreditoRepository, CreditoRepository>();
builder.Services.AddScoped<ICreditoService, CreditoService>();

builder.Services.AddScoped<IContenidoPremiumRepository, ContenidoPremiumRepository>();
builder.Services.AddScoped<IContenidoPremiumService, ContenidoPremiumService>();

builder.Services.AddScoped<IPrivacidadRepository, PrivacidadRepository>();
builder.Services.AddScoped<IPrivacidadService, PrivacidadService>();

builder.Services.AddScoped<IListaDeReproduccionRepository, ListaDeReproduccionRepository>();
builder.Services.AddScoped<IListaDeReproduccionService, ListaDeReproduccionService>();

builder.Services.AddScoped<IFavoritoRepository, FavoritoRepository>();
builder.Services.AddScoped<IFavoritoService, FavoritoService>();

// Configurar Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "GrooveArchive API",
        Version = "v1",
        Description = "API para el proyecto GrooveArchive"
    });
    // Opcional: Configuración de seguridad para Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Por favor ingresa 'Bearer' seguido de un espacio y el token JWT",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement{
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference{ Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            new string[] {}
        }
    });
});


// Agregar controladores
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurar el pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "GrooveArchive API V1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseCors("AllowAllOrigins"); // Activar CORS

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

