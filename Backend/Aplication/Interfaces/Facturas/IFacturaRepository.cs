using Domain.Entities;


namespace Aplication.Interfaces.Facturas
{
    public interface IFacturaRepository
    {
        Task<IEnumerable<Factura>> GetAllAsync();
        Task<Factura> GetByIdAsync(int id);
        Task<Factura> CreateAsync(Factura factura);
        Task<bool> UpdateAsync(Factura factura);
        Task<bool> DeleteAsync(int id);
    }
}
