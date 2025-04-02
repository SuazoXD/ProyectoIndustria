using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.MetodosPago
{
    public interface IMetodoPagoRepository
    {
        Task<IEnumerable<MetodoPago>> GetAllAsync();
        Task<MetodoPago> GetByIdAsync(int id);
        Task<MetodoPago> CreateAsync(MetodoPago metodoPago);
        Task<bool> UpdateAsync(MetodoPago metodoPago);
        Task<bool> DeleteAsync(int id);
    }
}
