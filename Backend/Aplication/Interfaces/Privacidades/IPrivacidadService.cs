using Application.DTOs.Privacidad;
using Application.DTOs.Privacidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Privacidades
{
    public interface IPrivacidadService
    {
        Task<IEnumerable<PrivacidadResponseDTO>> GetAllAsync();
        Task<PrivacidadResponseDTO> GetByIdAsync(int id);
        Task<PrivacidadResponseDTO> CreateAsync(PrivacidadRequestDTO dto);
        Task<bool> UpdateAsync(int id, PrivacidadRequestDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
