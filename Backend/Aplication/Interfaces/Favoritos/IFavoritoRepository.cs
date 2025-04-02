using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Favoritos
{
    public interface IFavoritoRepository
    {
        Task<IEnumerable<Favorito>> GetAllAsync();
        Task<Favorito> GetByIdAsync(int id);
        Task<Favorito> CreateAsync(Favorito favorito);
        Task<bool> UpdateAsync(Favorito favorito);
        Task<bool> DeleteAsync(int id);
    }
}
