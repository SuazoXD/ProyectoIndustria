using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Repositories.Archivos;
using Infrastructure.Repositories.ContenidosPremium;
using Infrastructure.Repositories.Creditos;
using Infrastructure.Repositories.Facturas;
using Infrastructure.Repositories.Favoritos;
using Infrastructure.Repositories.ListasDeReproduccion;
using Infrastructure.Repositories.Pagos;
using Infrastructure.Repositories.Permisos;
using Infrastructure.Repositories.Privacidades;
using Infrastructure.Repositories.Usuarios;
using Aplication.Interfaces.Archivos;
using Aplication.Interfaces.ContenidosPremium;
using Aplication.Interfaces.Creditos;
using Aplication.Interfaces.Facturas;
using Aplication.Interfaces.Favoritos;
using Aplication.Interfaces.ListasDeReproduccion;
using Aplication.Interfaces.Pagos;
using Aplication.Interfaces.Permisos;
using Aplication.Interfaces.Privacidades;
using Aplication.Interfaces.Usuarios;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Configurar DbContext con la cadena de conexión definida en appsettings.json
            services.AddDbContext<ProjectDBContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Registrar repositorios
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
