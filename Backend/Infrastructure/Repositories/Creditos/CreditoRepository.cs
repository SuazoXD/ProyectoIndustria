using Aplication.Interfaces.Creditos;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories.Creditos
{
    public class CreditoRepository : ICreditoRepository
    {
        private readonly ProjectDBContext _context;

        public CreditoRepository(ProjectDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Credito>> GetAllAsync() =>
            await _context.Creditos.ToListAsync();

        public async Task<Credito> GetByIdAsync(int id) =>
            await _context.Creditos.FindAsync(id);

        public async Task<Credito> CreateAsync(Credito credito)
        {
            _context.Creditos.Add(credito);
            await _context.SaveChangesAsync();
            return credito;
        }

        public async Task<bool> UpdateAsync(Credito credito)
        {
            _context.Creditos.Update(credito);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var credito = await _context.Creditos.FindAsync(id);
            if (credito == null)
                return false;
            _context.Creditos.Remove(credito);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
