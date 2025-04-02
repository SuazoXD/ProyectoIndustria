using Aplication.DTOs.Permisos;
using Aplication.Interfaces.Permisos;
using Domain.Entities;


namespace Application.Services.Permisos
{
    public class PermisoService : IPermisoService
    {
        private readonly IPermisoRepository _permisoRepository;

        public PermisoService(IPermisoRepository permisoRepository)
        {
            _permisoRepository = permisoRepository;
        }

        public async Task<IEnumerable<PermisoResponseDTO>> GetAllAsync()
        {
            var permisos = await _permisoRepository.GetAllAsync();
            return permisos.Select(p => new PermisoResponseDTO
            {
                Id = p.Id,
                Descripcion = p.Descripcion
            });
        }

        public async Task<PermisoResponseDTO> GetByIdAsync(int id)
        {
            var permiso = await _permisoRepository.GetByIdAsync(id);
            if (permiso == null) return null;
            return new PermisoResponseDTO
            {
                Id = permiso.Id,
                Descripcion = permiso.Descripcion
            };
        }

        public async Task<PermisoResponseDTO> CreateAsync(PermisoRequestDTO dto)
        {
            var permiso = new Permiso(dto.Descripcion);
            var created = await _permisoRepository.CreateAsync(permiso);
            return new PermisoResponseDTO
            {
                Id = created.Id,
                Descripcion = created.Descripcion
            };
        }

        public async Task<bool> UpdateAsync(int id, PermisoRequestDTO dto)
        {
            var permiso = await _permisoRepository.GetByIdAsync(id);
            if (permiso == null)
                return false;

            // Usar el método UpdateDescripcion en lugar de asignar directamente la propiedad
            permiso.UpdateDescripcion(dto.Descripcion);

            return await _permisoRepository.UpdateAsync(permiso);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _permisoRepository.DeleteAsync(id);
        }
    }
}
