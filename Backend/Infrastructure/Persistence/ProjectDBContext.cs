
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ProjectDBContext : DbContext
    {
        public ProjectDBContext(DbContextOptions<ProjectDBContext> options) : base(options)
        {
        }

        // Agregamos el DbSet para Product
      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Aquí puedes configurar las entidades, relaciones, etc.
        }
    }
}
