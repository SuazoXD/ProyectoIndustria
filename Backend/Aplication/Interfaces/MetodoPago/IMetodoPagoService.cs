using Application.DTOs.MetodoPago;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.MetodosPago
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

