
using Aplication.DTOs.Archivo;

namespace Aplication.Interfaces.Archivos
{
    public interface IArchivoService
    {
        Task<IEnumerable<ArchivoResponseDTO>> GetAllAsync();
        Task<ArchivoResponseDTO> GetByIdAsync(int id);
        Task<ArchivoResponseDTO> CreateAsync(ArchivoRequestDTO dto);
        Task<bool> UpdateAsync(int id, ArchivoRequestDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
