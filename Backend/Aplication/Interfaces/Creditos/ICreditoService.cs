
using Aplication.DTOs.Creditos;

namespace Aplication.Interfaces.Creditos
{
    public interface ICreditoService
    {
        Task<IEnumerable<CreditoResponseDTO>> GetAllAsync();
        Task<CreditoResponseDTO> GetByIdAsync(int id);
        Task<CreditoResponseDTO> CreateAsync(CreditoRequestDTO dto);
        Task<bool> UpdateAsync(int id, CreditoRequestDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}

