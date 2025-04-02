using Aplication.DTOs.Pago;

namespace Aplication.Interfaces.Pagos
{
    public interface IPagoService
    {
        Task<IEnumerable<PagoResponseDTO>> GetAllAsync();
        Task<PagoResponseDTO> GetByIdAsync(int id);
        Task<PagoResponseDTO> CreateAsync(PagoRequestDTO dto);
        Task<bool> UpdateAsync(int id, PagoRequestDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
