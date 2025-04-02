using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.MetodoPago
{
    public class MetodoPagoRepository : IMetodoPagoRepository
    {
        private readonly ProjectDBContext _context;

        public MetodoPagoRepository(ProjectDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MetodoPago>> GetAllAsync() =>
            await _context.MetodosPago.ToListAsync();

        public async Task<MetodoPago> GetByIdAsync(int id) =>
            await _context.MetodosPago.FindAsync(id);

        public async Task<MetodoPago> CreateAsync(MetodoPago metodoPago)
        {
            _context.MetodosPago.Add(metodoPago);
            await _context.SaveChangesAsync();
            return metodoPago;
        }

        public async Task<bool> UpdateAsync(MetodoPago metodoPago)
        {
            _context.MetodosPago.Update(metodoPago);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var metodo = await _context.MetodosPago.FindAsync(id);
            if (metodo == null)
                return false;
            _context.MetodosPago.Remove(metodo);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

