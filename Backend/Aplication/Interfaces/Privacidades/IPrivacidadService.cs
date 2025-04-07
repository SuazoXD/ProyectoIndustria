using Aplication.DTOs.Privacidades;


namespace Aplication.Interfaces.Privacidades
{
    public interface IPrivacidadService
    {
        Task<IEnumerable<PrivacidadResponseDTO>> GetAllAsync();
        Task<PrivacidadResponseDTO> GetByIdAsync(int id);
        Task<PrivacidadResponseDTO> CreateAsync(PrivacidadRequestDTO dto);
        Task<bool> UpdateAsync(int id, PrivacidadRequestDTO dto);
        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<PrivacidadResponseDTO>> GetByUserIdAsync(int userId);

    }
}

