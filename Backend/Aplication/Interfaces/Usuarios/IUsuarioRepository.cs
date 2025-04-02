using Domain.AggregateRoots;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Usuarios
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario> GetByIdAsync(int id);
        Task<Usuario> CreateAsync(Usuario usuario);
        Task<bool> UpdateAsync(Usuario usuario);
        Task<bool> DeleteAsync(int id);
    }
}
