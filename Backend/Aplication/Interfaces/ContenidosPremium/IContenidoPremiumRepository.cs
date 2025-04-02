
using Aplication.DTOs.ArchivoListaReproduccion;

namespace Aplication.Interfaces.ContenidosPremium
{
    public interface IArchivoListaReproduccionService
    {
        Task<IEnumerable<ArchivoListaReproduccionResponseDTO>> GetAllAsync();
        Task<ArchivoListaReproduccionResponseDTO> GetByIdAsync(int id);
        Task<ArchivoListaReproduccionResponseDTO> CreateAsync(ArchivoListaReproduccionRequestDTO dto);
        Task<bool> UpdateAsync(int id, ArchivoListaReproduccionRequestDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
