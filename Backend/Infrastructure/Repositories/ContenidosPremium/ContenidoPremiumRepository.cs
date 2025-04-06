using Aplication.Interfaces.ContenidosPremium;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore; // Necesario para ToListAsync
using System.Linq; // Opcional si usas LINQ


namespace Infrastructure.Repositories.ContenidosPremium
{
    public class ContenidoPremiumRepository : IContenidoPremiumRepository
    {
        private readonly ProjectDBContext _context;
        public ContenidoPremiumRepository(ProjectDBContext context) => _context = context;

        public async Task<IEnumerable<ContenidoPremium>> GetAllAsync()
        {
            return await _context.ContenidoPremium
               .Include(p => p.Usuario)
               .ToListAsync();

        }
        public async Task<ContenidoPremium> GetByIdAsync(int id) =>
            await _context.ContenidoPremium.FindAsync(id);

        public async Task<ContenidoPremium> CreateAsync(ContenidoPremium contenido)
        {
            _context.ContenidoPremium.Add(contenido);
            await _context.SaveChangesAsync();
            return contenido;
        }

        public async Task<bool> UpdateAsync(ContenidoPremium contenido)
        {
            _context.ContenidoPremium.Update(contenido);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var contenido = await _context.ContenidoPremium.FindAsync(id);
            if (contenido == null) return false;
            _context.ContenidoPremium.Remove(contenido);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
