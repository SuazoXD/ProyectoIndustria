using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Permisos
{
    public interface IPermisoRepository
    {
        Task<IEnumerable<Permiso>> GetAllAsync();
        Task<Permiso> GetByIdAsync(int id);
        Task<Permiso> CreateAsync(Permiso permiso);
        Task<bool> UpdateAsync(Permiso permiso);
        Task<bool> DeleteAsync(int id);
    }
}
