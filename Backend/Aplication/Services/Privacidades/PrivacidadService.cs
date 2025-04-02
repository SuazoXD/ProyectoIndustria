using Application.DTOs.Privacidad;
using Application.Interfaces.Privacidades;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.Privacidades
{
    public class PrivacidadService : IPrivacidadService
    {
        private readonly IPrivacidadRepository _privacidadRepository;

        public PrivacidadService(IPrivacidadRepository privacidadRepository)
        {
            _privacidadRepository = privacidadRepository;
        }

        public async Task<IEnumerable<PrivacidadResponseDTO>> GetAllAsync()
        {
            var registros = await _privacidadRepository.GetAllAsync();
            return registros.Select(p => new PrivacidadResponseDTO
            {
                Id = p.Id,
                IdUsuario = p.IdUsuario,
                IdArchivo = p.IdArchivo,
                PermisoId = p.PermisoId,
                Autodestruccion = p.Autodestruccion,
                DispositivosPermitidos = p.DispositivosPermitidos
            });
        }

        public async Task<PrivacidadResponseDTO> GetByIdAsync(int id)
        {
            var registro = await _privacidadRepository.GetByIdAsync(id);
            if (registro == null)
                return null;
            return new PrivacidadResponseDTO
            {
                Id = registro.Id,
                IdUsuario = registro.IdUsuario,
                IdArchivo = registro.IdArchivo,
                PermisoId = registro.PermisoId,
                Autodestruccion = registro.Autodestruccion,
                DispositivosPermitidos = registro.DispositivosPermitidos
            };
        }

        public async Task<PrivacidadResponseDTO> CreateAsync(PrivacidadRequestDTO dto)
        {
            // Asumimos que la entidad Privacidad tiene un constructor público que acepta estos parámetros,
            // o bien, se crea mediante Activator.CreateInstance si es necesario.
            var registro = new Privacidad(dto.IdUsuario, dto.IdArchivo, dto.PermisoId, dto.Autodestruccion, dto.DispositivosPermitidos);
            var created = await _privacidadRepository.CreateAsync(registro);
            return new PrivacidadResponseDTO
            {
                Id = created.Id,
                IdUsuario = created.IdUsuario,
                IdArchivo = created.IdArchivo,
                PermisoId = created.PermisoId,
                Autodestruccion = created.Autodestruccion,
                DispositivosPermitidos = created.DispositivosPermitidos
            };
        }

        public async Task<bool> UpdateAsync(int id, PrivacidadRequestDTO dto)
        {
            var registro = await _privacidadRepository.GetByIdAsync(id);
            if (registro == null)
                return false;

            // Actualiza las propiedades según tu lógica (usa reflection si los setters son inaccesibles)
            // Ejemplo con reflection:
            var type = typeof(Privacidad);
            type.GetProperty("IdUsuario", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)
                .SetValue(registro, dto.IdUsuario);
            type.GetProperty("IdArchivo", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)
                .SetValue(registro, dto.IdArchivo);
            type.GetProperty("PermisoId", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)
                .SetValue(registro, dto.PermisoId);
            type.GetProperty("Autodestruccion", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)
                .SetValue(registro, dto.Autodestruccion);
            type.GetProperty("DispositivosPermitidos", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)
                .SetValue(registro, dto.DispositivosPermitidos);

            return await _privacidadRepository.UpdateAsync(registro);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _privacidadRepository.DeleteAsync(id);
        }
    }
}
