using Application.DTOs.Usuario;
using Application.Interfaces.Usuarios;
using Domain.AggregateRoots;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.Usuarios
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IEnumerable<UsuarioResponseDTO>> GetAllAsync()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            return usuarios.Select(u => new UsuarioResponseDTO
            {
                Id = u.Id,
                NombreUsuario = u.NombreUsuario,
                Correo = u.Correo,
                FechaRegistro = u.FechaRegistro
            });
        }

        public async Task<UsuarioResponseDTO> GetByIdAsync(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null) return null;
            return new UsuarioResponseDTO
            {
                Id = usuario.Id,
                NombreUsuario = usuario.NombreUsuario,
                Correo = usuario.Correo,
                FechaRegistro = usuario.FechaRegistro
            };
        }

        public async Task<UsuarioResponseDTO> CreateAsync(UsuarioRequestDTO dto)
        {
            // Crear un nuevo usuario
            var usuario = new Usuario(dto.NombreUsuario, dto.Contrasenia, dto.Correo, dto.IdRol);
            var created = await _usuarioRepository.CreateAsync(usuario);
            return new UsuarioResponseDTO
            {
                Id = created.Id,
                NombreUsuario = created.NombreUsuario,
                Correo = created.Correo,
                FechaRegistro = created.FechaRegistro
            };
        }

        public async Task<bool> UpdateAsync(int id, UsuarioRequestDTO dto)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null)
                return false;

            // Aquí actualizamos las propiedades de la entidad sin modificar directamente el setter privado
            // Usamos un método de actualización específico en el servicio, sin tocar la clase Usuario

            // Usamos reflexión para actualizar las propiedades privadas
            if (!string.IsNullOrEmpty(dto.NombreUsuario))
                usuario.GetType().GetProperty("NombreUsuario").SetValue(usuario, dto.NombreUsuario);

            if (!string.IsNullOrEmpty(dto.Correo))
                usuario.GetType().GetProperty("Correo").SetValue(usuario, dto.Correo);

            if (!string.IsNullOrEmpty(dto.Contrasenia))
                usuario.GetType().GetProperty("Contrasenia").SetValue(usuario, dto.Contrasenia);

            return await _usuarioRepository.UpdateAsync(usuario);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _usuarioRepository.DeleteAsync(id);
        }
    }
}
