using Aplication.Interfaces.Facturas;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Facturas
{
    public class FacturaRepository : IFacturaRepository
    {
        private readonly ProjectDBContext _context;
        public FacturaRepository(ProjectDBContext context) => _context = context;

        public async Task<IEnumerable<Factura>> GetAllAsync() =>
            await _context.Facturas.ToListAsync();

        public async Task<Factura> GetByIdAsync(int id) =>
            await _context.Facturas.FindAsync(id);

        public async Task<Factura> CreateAsync(Factura factura)
        {
            _context.Facturas.Add(factura);
            await _context.SaveChangesAsync();
            return factura;
        }

        public async Task<bool> UpdateAsync(Factura factura)
        {
            _context.Facturas.Update(factura);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var factura = await _context.Facturas.FindAsync(id);
            if (factura == null) return false;
            _context.Facturas.Remove(factura);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
