
using Domain.Entities;

namespace Aplication.Interfaces.Creditos
{
    public interface ICreditoRepository
    {
        Task<IEnumerable<Credito>> GetAllAsync();
        Task<Credito> GetByIdAsync(int id);
        Task<Credito> CreateAsync(Credito credito);
        Task<bool> UpdateAsync(Credito credito);
        Task<bool> DeleteAsync(int id);
    }
}
