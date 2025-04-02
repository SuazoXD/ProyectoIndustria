using Aplication.DTOs.Permisos;


namespace Aplication.Interfaces.Permisos
{
    public interface IPermisoService
    {
        Task<IEnumerable<PermisoResponseDTO>> GetAllAsync();
        Task<PermisoResponseDTO> GetByIdAsync(int id);
        Task<PermisoResponseDTO> CreateAsync(PermisoRequestDTO dto);
        Task<bool> UpdateAsync(int id, PermisoRequestDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
