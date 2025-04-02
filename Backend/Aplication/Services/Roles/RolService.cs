using Aplication.DTOs.Rol;
using Aplication.Interfaces.Roles;
using Domain.AggregateRoots;
using System.Reflection;


namespace Aplication.Services.Roles
{
    public class RolService : IRolService
    {
        private readonly IRolRepository _rolRepository;

        public RolService(IRolRepository rolRepository)
        {
            _rolRepository = rolRepository;
        }

        public async Task<IEnumerable<RolResponseDTO>> GetAllAsync()
        {
            var roles = await _rolRepository.GetAllAsync();
            return roles.Select(r => new RolResponseDTO
            {
                Id = r.Id,
                NombreRol = r.NombreRol,
                Descripcion = r.Descripcion
            });
        }

        public async Task<RolResponseDTO> GetByIdAsync(int id)
        {
            var rol = await _rolRepository.GetByIdAsync(id);
            if (rol == null) return null;
            return new RolResponseDTO
            {
                Id = rol.Id,
                NombreRol = rol.NombreRol,
                Descripcion = rol.Descripcion
            };
        }

        public async Task<RolResponseDTO> CreateAsync(RolRequestDTO dto)
        {
            var rol = new Rol(dto.NombreRol, dto.Descripcion);
            var created = await _rolRepository.CreateAsync(rol);
            return new RolResponseDTO
            {
                Id = created.Id,
                NombreRol = created.NombreRol,
                Descripcion = created.Descripcion
            };
        }

        public async Task<bool> UpdateAsync(int id, RolRequestDTO dto)
        {
            var rol = await _rolRepository.GetByIdAsync(id);
            if (rol == null)
                return false;

            // Actualizar propiedades privadas con reflection
            var rolType = typeof(Rol);
            var nombreProp = rolType.GetProperty("NombreRol", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            var descripcionProp = rolType.GetProperty("Descripcion", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            if (nombreProp != null && nombreProp.CanWrite)
                nombreProp.SetValue(rol, dto.NombreRol);
            if (descripcionProp != null && descripcionProp.CanWrite)
                descripcionProp.SetValue(rol, dto.Descripcion);

            return await _rolRepository.UpdateAsync(rol);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _rolRepository.DeleteAsync(id);
        }
    }
}
