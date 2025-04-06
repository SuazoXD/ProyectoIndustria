using Aplication.Interfaces.Roles;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Roles
{
    public class RolRepository : IRolRepository
    {
        private readonly ProjectDBContext _context;

        public RolRepository(ProjectDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rol>> GetAllAsync() =>
            await _context.Roles.ToListAsync();

        public async Task<Rol> GetByIdAsync(int id) =>
            await _context.Roles.FindAsync(id);

        public async Task<Rol> CreateAsync(Rol rol)
        {
            _context.Roles.Add(rol);
            await _context.SaveChangesAsync();
            return rol;
        }

        public async Task<bool> UpdateAsync(Rol rol)
        {
            _context.Roles.Update(rol);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var rol = await _context.Roles.FindAsync(id);
            if (rol == null)
                return false;
            _context.Roles.Remove(rol);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
