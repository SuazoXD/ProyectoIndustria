
using Domain.Entities;

namespace Aplication.Interfaces.ArchivosListaReproduccion
{
    public interface IArchivoListaReproduccionRepository
    {
        Task<IEnumerable<ArchivoListaReproduccion>> GetAllAsync();
        Task<ArchivoListaReproduccion> GetByIdAsync(int id);
        Task<ArchivoListaReproduccion> CreateAsync(ArchivoListaReproduccion entity);
        Task<bool> UpdateAsync(ArchivoListaReproduccion entity);
        Task<bool> DeleteAsync(int id);
    }
}
