using Aplication.Interfaces.Permisos;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories.Permisos
{
    public class PermisoRepository : IPermisoRepository
    {
        private readonly ProjectDBContext _context;

        public PermisoRepository(ProjectDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Permiso>> GetAllAsync() =>
            await _context.Permisos.ToListAsync();

        public async Task<Permiso> GetByIdAsync(int id) =>
            await _context.Permisos.FindAsync(id);

        public async Task<Permiso> CreateAsync(Permiso permiso)
        {
            _context.Permisos.Add(permiso);
            await _context.SaveChangesAsync();
            return permiso;
        }

        public async Task<bool> UpdateAsync(Permiso permiso)
        {
            _context.Permisos.Update(permiso);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var permiso = await _context.Permisos.FindAsync(id);
            if (permiso == null)
                return false;
            _context.Permisos.Remove(permiso);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
