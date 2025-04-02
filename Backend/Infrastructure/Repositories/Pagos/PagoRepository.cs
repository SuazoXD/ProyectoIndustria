using Application.Interfaces;
using Domain.AggregateRoots;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

<<<<<<<< HEAD:Backend/Infrastructure/Repositories/Pago/PagoRepository.cs
namespace Infrastructure.Repositories.Pago
========
namespace Infrastructure.Repositories.Pagos
>>>>>>>> 6aa8cbc (ADD FIX:Infrastructure Repository):Backend/Infrastructure/Repositories/Pagos/PagoRepository.cs
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
