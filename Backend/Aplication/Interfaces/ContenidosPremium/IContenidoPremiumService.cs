
using Aplication.DTOs.ContenidosPremium;

namespace Aplication.Interfaces.ContenidosPremium
{
 public interface IContenidoPremiumService
    {
        Task<IEnumerable<ContenidoPremiumResponseDTO>> GetAllAsync();
        Task<ContenidoPremiumResponseDTO> GetByIdAsync(int id);
        Task<ContenidoPremiumResponseDTO> CreateAsync(ContenidoPremiumRequestDTO dto);
        Task<bool> UpdateAsync(int id, ContenidoPremiumRequestDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
