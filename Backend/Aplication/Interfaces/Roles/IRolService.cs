using Aplication.DTOs.Rol;
using Application.DTOs.Rol;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Roles
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
