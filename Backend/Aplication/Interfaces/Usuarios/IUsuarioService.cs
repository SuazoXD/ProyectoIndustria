using System.Collections.Generic;
using System.Threading.Tasks;
using Aplication.DTOs.Usuarios;

namespace Application.Interfaces.Usuarios
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioResponseDTO>> GetAllAsync();
        Task<UsuarioResponseDTO> GetByIdAsync(int id);
        Task<UsuarioResponseDTO> CreateAsync(UsuarioRequestDTO dto);
        Task<bool> UpdateAsync(int id, UsuarioRequestDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
