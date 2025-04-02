
using Aplication.DTOs.ContenidosPremium;
using Aplication.Interfaces.ContenidosPremium;
using Domain.Entities;

namespace Aplication.Services.ContenidosPremium
{
    public class ContenidoPremiumService : IContenidoPremiumService
    {
        private readonly IContenidoPremiumRepository _contenidoPremiumRepository;

        public ContenidoPremiumService(IContenidoPremiumRepository contenidoPremiumRepository)
        {
            _contenidoPremiumRepository = contenidoPremiumRepository;
        }

        public async Task<IEnumerable<ContenidoPremiumResponseDTO>> GetAllAsync()
        {
            var contenidos = await _contenidoPremiumRepository.GetAllAsync();
            return contenidos.Select(cp => new ContenidoPremiumResponseDTO
            {
                Id = cp.Id,
                NombreContenido = cp.NombreContenido,
                TipoContenido = cp.TipoContenido,
                IdUsuario = cp.IdUsuario,
                Precio = cp.Precio
            });
        }

        public async Task<ContenidoPremiumResponseDTO> GetByIdAsync(int id)
        {
            var contenido = await _contenidoPremiumRepository.GetByIdAsync(id);
            if (contenido == null)
                return null;
            return new ContenidoPremiumResponseDTO
            {
                Id = contenido.Id,
                NombreContenido = contenido.NombreContenido,
                TipoContenido = contenido.TipoContenido,
                IdUsuario = contenido.IdUsuario,
                Precio = contenido.Precio
            };
        }

        public async Task<ContenidoPremiumResponseDTO> CreateAsync(ContenidoPremiumRequestDTO dto)
        {
            // Asumiendo que la entidad ContenidoPremium tiene un constructor público que acepta (string, string, int, decimal)
            var contenido = new ContenidoPremium(dto.NombreContenido, dto.TipoContenido, dto.IdUsuario, dto.Precio);
            var created = await _contenidoPremiumRepository.CreateAsync(contenido);
            return new ContenidoPremiumResponseDTO
            {
                Id = created.Id,
                NombreContenido = created.NombreContenido,
                TipoContenido = created.TipoContenido,
                IdUsuario = created.IdUsuario,
                Precio = created.Precio
            };
        }

        public async Task<bool> UpdateAsync(int id, ContenidoPremiumRequestDTO dto)
        {
            var contenido = await _contenidoPremiumRepository.GetByIdAsync(id);
            if (contenido == null)
                return false;

            // Si las propiedades tienen setters privados, puedes usar reflection para actualizar.
            var type = typeof(ContenidoPremium);
            type.GetProperty("NombreContenido", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)
                .SetValue(contenido, dto.NombreContenido);
            type.GetProperty("TipoContenido", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)
                .SetValue(contenido, dto.TipoContenido);
            type.GetProperty("Precio", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)
                .SetValue(contenido, dto.Precio);

            return await _contenidoPremiumRepository.UpdateAsync(contenido);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _contenidoPremiumRepository.DeleteAsync(id);
        }
    }
}

