using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Application.Interfaces;
using Infrastructure.Repositories.Privacidades;
using Infrastructure.Repositories.Permisos;
using Infrastructure.Repositories.Usuarios;
using Infrastructure.Repositories.Archivos;
using Infrastructure.Repositories.ContenidoPremium;
using Infrastructure.Repositories.Creditos;
using Infrastructure.Repositories.Factura;
using Infrastructure.Repositories.Favorito;
using Infrastructure.Repositories.ListaDeReproduccion;
using Infrastructure.Repositories.Pago;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ProjectDBContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IArchivoRepository, ArchivoRepository>();
            services.AddScoped<IPagoRepository, PagoRepository>();
            services.AddScoped<IFacturaRepository, FacturaRepository>();
            services.AddScoped<ICreditoRepository, CreditoRepository>();
            services.AddScoped<IContenidoPremiumRepository, ContenidoPremiumRepository>();
            services.AddScoped<IPermisoRepository, PermisoRepository>();
            services.AddScoped<IPrivacidadRepository, PrivacidadRepository>();
            services.AddScoped<IListaDeReproduccionRepository, ListaDeReproduccionRepository>();
            services.AddScoped<IFavoritoRepository, FavoritoRepository>();

            return services;
        }
    }
}
