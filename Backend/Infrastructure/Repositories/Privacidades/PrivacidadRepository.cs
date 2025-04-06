using Aplication.Interfaces.Privacidades;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories.Privacidades
{
    public class PrivacidadRepository : IPrivacidadRepository
    {
        private readonly ProjectDBContext _context;

        public PrivacidadRepository(ProjectDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Privacidad>> GetAllAsync()
        {
            return await _context.Privacidad
               .Include(p => p.Usuario)
               .Include(p => p.Archivo)
               .Include(p => p.Permiso)
               .ToListAsync();

        }

        public async Task<Privacidad> GetByIdAsync(int id) =>
            await _context.Privacidad.FindAsync(id);

        public async Task<Privacidad> CreateAsync(Privacidad privacidad)
        {
            _context.Privacidad.Add(privacidad);
            await _context.SaveChangesAsync();
            return privacidad;
        }

        public async Task<bool> UpdateAsync(Privacidad privacidad)
        {
            _context.Privacidad.Update(privacidad);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var privacidad = await _context.Privacidad.FindAsync(id);
            if (privacidad == null)
                return false;
            _context.Privacidad.Remove(privacidad);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
