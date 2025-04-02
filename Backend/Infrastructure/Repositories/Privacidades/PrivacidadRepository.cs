using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Privacidades
{
    public class PrivacidadRepository : IPrivacidadRepository
    {
        private readonly ProjectDBContext _context;

        public PrivacidadRepository(ProjectDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Privacidad>> GetAllAsync() =>
            await _context.Privacidad.ToListAsync();

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
