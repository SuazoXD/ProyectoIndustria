using Application.DTOs.MetodoPago;
using Application.Interfaces.MetodosPago;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.MetodosPago
{
    public class MetodoPagoService : IMetodoPagoService
    {
        private readonly IMetodoPagoRepository _metodoPagoRepository;

        public MetodoPagoService(IMetodoPagoRepository metodoPagoRepository)
        {
            _metodoPagoRepository = metodoPagoRepository;
        }

        public async Task<IEnumerable<MetodoPagoResponseDTO>> GetAllAsync()
        {
            var metodos = await _metodoPagoRepository.GetAllAsync();
            return metodos.Select(mp => new MetodoPagoResponseDTO
            {
                Id = mp.Id,
                NombreMetodo = mp.NombreMetodo,
                Activo = mp.Activo
            });
        }

        public async Task<MetodoPagoResponseDTO> GetByIdAsync(int id)
        {
            var metodo = await _metodoPagoRepository.GetByIdAsync(id);
            if (metodo == null) return null;
            return new MetodoPagoResponseDTO
            {
                Id = metodo.Id,
                NombreMetodo = metodo.NombreMetodo,
                Activo = metodo.Activo
            };
        }

        public async Task<MetodoPagoResponseDTO> CreateAsync(MetodoPagoRequestDTO dto)
        {
            var metodo = new MetodoPago(dto.NombreMetodo, dto.Activo);
            var created = await _metodoPagoRepository.CreateAsync(metodo);
            return new MetodoPagoResponseDTO
            {
                Id = created.Id,
                NombreMetodo = created.NombreMetodo,
                Activo = created.Activo
            };
        }

        public async Task<bool> UpdateAsync(int id, MetodoPagoRequestDTO dto)
        {
            var metodo = await _metodoPagoRepository.GetByIdAsync(id);
            if (metodo == null) return false;
            // Actualización simple: actualizar propiedades directamente
            // Se asume que el repositorio se encargará de persistir cambios
            metodo.GetType().GetProperty("NombreMetodo").SetValue(metodo, dto.NombreMetodo);
            metodo.GetType().GetProperty("Activo").SetValue(metodo, dto.Activo);
            return await _metodoPagoRepository.UpdateAsync(metodo);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _metodoPagoRepository.DeleteAsync(id);
        }
    }
}
