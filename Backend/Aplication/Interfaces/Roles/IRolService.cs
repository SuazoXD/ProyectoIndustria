using Aplication.DTOs.Rol;


namespace Aplication.Interfaces.Roles
{
    public interface IRolService
    {
        Task<IEnumerable<RolResponseDTO>> GetAllAsync();
        Task<RolResponseDTO> GetByIdAsync(int id);
        Task<RolResponseDTO> CreateAsync(RolRequestDTO dto);
        Task<bool> UpdateAsync(int id, RolRequestDTO dto);
        Task<bool> DeleteAsync(int id);

    }


}
