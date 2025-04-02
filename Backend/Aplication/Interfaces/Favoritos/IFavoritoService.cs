using Application.DTOs.Favorito;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Favoritos
{
    public interface IFavoritoService
    {
        Task<IEnumerable<FavoritoResponseDTO>> GetAllAsync();
        Task<FavoritoResponseDTO> GetByIdAsync(int id);
        Task<FavoritoResponseDTO> CreateAsync(FavoritoRequestDTO dto);
        Task<bool> UpdateAsync(int id, FavoritoRequestDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
