using Aplication.Interfaces.Pagos;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories.Pagos
{
    public class PagoRepository : IPagoRepository
    {
        private readonly ProjectDBContext _context;

        public PagoRepository(ProjectDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pago>> GetAllAsync() =>
            await _context.Pagos.ToListAsync();

        public async Task<Pago> GetByIdAsync(int id) =>
            await _context.Pagos.FindAsync(id);

        public async Task<Pago> CreateAsync(Pago pago)
        {
            _context.Pagos.Add(pago);
            await _context.SaveChangesAsync();
            return pago;
        }

        public async Task<bool> UpdateAsync(Pago pago)
        {
            _context.Pagos.Update(pago);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var pago = await _context.Pagos.FindAsync(id);
            if (pago == null) return false;
            _context.Pagos.Remove(pago);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
