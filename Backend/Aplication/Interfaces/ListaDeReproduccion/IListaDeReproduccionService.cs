using Aplication.DTOs.ListaDeReproduccion;


namespace Aplication.Interfaces.ListasDeReproduccion
{
    public interface IListaDeReproduccionService
    {
        Task<IEnumerable<ListaDeReproduccionResponseDTO>> GetAllAsync();
        Task<ListaDeReproduccionResponseDTO> GetByIdAsync(int id);
        Task<ListaDeReproduccionResponseDTO> CreateAsync(ListaDeReproduccionRequestDTO dto);
        Task<bool> UpdateAsync(int id, ListaDeReproduccionRequestDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
