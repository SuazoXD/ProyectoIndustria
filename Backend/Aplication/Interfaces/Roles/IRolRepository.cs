using Domain.AggregateRoots;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Roles
{
    public interface IRolRepository
    {
        Task<IEnumerable<Rol>> GetAllAsync();
        Task<Rol> GetByIdAsync(int id);
        Task<Rol> CreateAsync(Rol rol);
        Task<bool> UpdateAsync(Rol rol);
        Task<bool> DeleteAsync(int id);
    }
}
