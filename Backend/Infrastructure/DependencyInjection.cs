using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Application.Interfaces;

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
