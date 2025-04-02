using Aplication.DTOs.MetodoPago;

namespace Aplication.Interfaces.MetodosPago
{
    public interface IMetodoPagoService
    {
        Task<IEnumerable<MetodoPagoResponseDTO>> GetAllAsync();
        Task<MetodoPagoResponseDTO> GetByIdAsync(int id);
        Task<MetodoPagoResponseDTO> CreateAsync(MetodoPagoRequestDTO dto);
        Task<bool> UpdateAsync(int id, MetodoPagoRequestDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}

