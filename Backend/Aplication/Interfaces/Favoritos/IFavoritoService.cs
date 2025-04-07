using Aplication.DTOs.Favorito;


namespace Aplication.Interfaces.Favoritos
{
    public interface IFavoritoService
    {
        Task<IEnumerable<FavoritoResponseDTO>> GetAllAsync();
        Task<FavoritoResponseDTO> GetByIdAsync(int id);
        Task<FavoritoResponseDTO> CreateAsync(FavoritoRequestDTO dto);
        Task<bool> UpdateAsync(int id, FavoritoRequestDTO dto);
        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<FavoritoResponseDTO>> GetAllByUserIdAsync(int userId);  // Nuevo método
    }
}
