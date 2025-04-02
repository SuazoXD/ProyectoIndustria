
using Microsoft.EntityFrameworkCore;
using Domain.AggregateRoots;  
using Domain.Entities;          

namespace Infrastructure.Persistence
{
    public class ProjectDBContext : DbContext
    {
        public ProjectDBContext(DbContextOptions<ProjectDBContext> options) : base(options)
        {
        }

        public DbSet<Privacidad> Privacidad { get; set; }
        public DbSet<ContenidoPremium> ContenidoPremium { get; set; }
        public DbSet<ArchivoListaReproduccion> Archivos_ListasDeReproduccion { get; set; }

       
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Archivo> Archivos { get; set; }
        public DbSet<ListaDeReproduccion> ListasDeReproduccion { get; set; }
        public DbSet<Pago> Pagos { get; set; }

      
        public DbSet<ArchivoListaReproduccion> ArchivosListasDeReproduccion { get; set; }
        public DbSet<ContenidoPremium> ContenidosPremium { get; set; }
        public DbSet<Credito> Creditos { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<Favorito> Favoritos { get; set; }
        public DbSet<MetodoPago> MetodosPago { get; set; }
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<Privacidad> Privacidades { get; set; }

       
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer("Server=localhost;Database=MyDatabase2;Trusted_Connection=True;");

            }
        }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Usuarios - Roles
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Rol)
                .WithMany(r => r.Usuarios)
                .HasForeignKey(u => u.IdRol)
                .OnDelete(DeleteBehavior.Restrict);

            // Archivos - Usuarios
            modelBuilder.Entity<Archivo>()
                .HasOne(a => a.Usuario)
                .WithMany(u => u.Archivos)
                .HasForeignKey(a => a.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            // Pagos - Usuarios
            modelBuilder.Entity<Pago>()
                .HasOne(p => p.Usuario)
                .WithMany(u => u.Pagos)
                .HasForeignKey(p => p.IdUsuario)
                .OnDelete(DeleteBehavior.Restrict);

            // Pagos - MetodosPago
            modelBuilder.Entity<Pago>()
                .HasOne(p => p.MetodoPagoEntity)
                .WithMany(mp => mp.Pagos)
                .HasForeignKey(p => p.MetodoPago)
                .OnDelete(DeleteBehavior.Restrict);

            // Facturas - Pagos
            modelBuilder.Entity<Factura>()
                .HasOne(f => f.Pago)
                .WithMany(p => p.Facturas)
                .HasForeignKey(f => f.IdPago)
                .OnDelete(DeleteBehavior.Cascade);

            // Creditos - Usuarios
            modelBuilder.Entity<Credito>()
                .HasOne(c => c.Usuario)
                .WithMany(u => u.Creditos)
                .HasForeignKey(c => c.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            // ContenidoPremium - Usuarios
            modelBuilder.Entity<ContenidoPremium>()
                .HasOne(cp => cp.Usuario)
                .WithMany(u => u.ContenidosPremium)
                .HasForeignKey(cp => cp.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            // Privacidad - Usuarios, Archivos, Permisos
            modelBuilder.Entity<Privacidad>()
                .HasOne(p => p.Usuario)
                .WithMany(u => u.Privacidades)
                .HasForeignKey(p => p.IdUsuario)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Privacidad>()
                .HasOne(p => p.Archivo)
                .WithMany(a => a.Privacidades)
                .HasForeignKey(p => p.IdArchivo)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Privacidad>()
                .HasOne(p => p.Permiso)
                .WithMany()
                .HasForeignKey(p => p.PermisoId)
                .OnDelete(DeleteBehavior.Restrict);

            // ListasDeReproduccion - Usuarios
            modelBuilder.Entity<ListaDeReproduccion>()
                .HasOne(l => l.Usuario)
                .WithMany(u => u.ListasDeReproduccion)
                .HasForeignKey(l => l.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            // Favoritos - Usuarios y Archivos
            modelBuilder.Entity<Favorito>()
                .HasOne(f => f.Usuario)
                .WithMany(u => u.Favoritos)
                .HasForeignKey(f => f.IdUsuario)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Favorito>()
                .HasOne(f => f.Archivo)
                .WithMany(a => a.Favoritos)
                .HasForeignKey(f => f.IdArchivo)
                .OnDelete(DeleteBehavior.Cascade);

            // Archivos_ListasDeReproduccion - ListasDeReproduccion y Archivos
            modelBuilder.Entity<ArchivoListaReproduccion>()
                .HasOne(al => al.ListaDeReproduccion)
                .WithMany(l => l.ArchivosListas)
                .HasForeignKey(al => al.IdLista)
                .OnDelete(DeleteBehavior.NoAction);  // Evita ciclos/múltiples cascadas

            modelBuilder.Entity<ArchivoListaReproduccion>()
                .HasOne(al => al.Archivo)
                .WithMany(a => a.ArchivosListas)
                .HasForeignKey(al => al.IdArchivo)
                .OnDelete(DeleteBehavior.NoAction);  // Opcional: usar NoAction o Restrict

            // Configuraciones adicionales:
            modelBuilder.Entity<Archivo>()
                .Property(a => a.TipoArchivo)
                .HasMaxLength(25);

            modelBuilder.Entity<ContenidoPremium>()
                .Property(cp => cp.Precio)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Factura>()
                .Property(f => f.TotalPagar)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Pago>()
                .Property(p => p.Monto)
                .HasPrecision(18, 2);
        }
    }
}
