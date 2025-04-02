using Aplication.DTOs.Factura;
using Aplication.Interfaces.Facturas;
using Domain.Entities;
using System.Reflection;

namespace Aplication.Services.Facturas
{
    public class FacturaService : IFacturaService
    {
        private readonly IFacturaRepository _facturaRepository;

        public FacturaService(IFacturaRepository facturaRepository)
        {
            _facturaRepository = facturaRepository;
        }

        public async Task<IEnumerable<FacturaResponseDTO>> GetAllAsync()
        {
            var facturas = await _facturaRepository.GetAllAsync();
            return facturas.Select(f => new FacturaResponseDTO
            {
                Id = f.Id,
                IdPago = f.IdPago,
                NumeroFactura = f.NumeroFactura,
                FechaEmision = f.FechaEmision,
                TotalPagar = f.TotalPagar,
                EstadoFactura = f.EstadoFactura
            });
        }

        public async Task<FacturaResponseDTO> GetByIdAsync(int id)
        {
            var factura = await _facturaRepository.GetByIdAsync(id);
            if (factura == null)
                return null;
            return new FacturaResponseDTO
            {
                Id = factura.Id,
                IdPago = factura.IdPago,
                NumeroFactura = factura.NumeroFactura,
                FechaEmision = factura.FechaEmision,
                TotalPagar = factura.TotalPagar,
                EstadoFactura = factura.EstadoFactura
            };
        }

        public async Task<FacturaResponseDTO> CreateAsync(FacturaRequestDTO dto)
        {
            // Crear la instancia de Factura utilizando reflection para invocar el constructor no público.
            var factura = (Factura)Activator.CreateInstance(typeof(Factura), nonPublic: true);

            // Asignar las propiedades a través de reflection.
            var type = typeof(Factura);
            type.GetProperty("IdPago", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .SetValue(factura, dto.IdPago);
            type.GetProperty("NumeroFactura", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .SetValue(factura, dto.NumeroFactura);
            type.GetProperty("TotalPagar", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .SetValue(factura, dto.TotalPagar);
            type.GetProperty("EstadoFactura", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .SetValue(factura, dto.EstadoFactura);
            // Asignar FechaEmision, se puede asignar la fecha actual si no viene en el DTO
            type.GetProperty("FechaEmision", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .SetValue(factura, DateTime.UtcNow);

            // Guardar la factura utilizando el repositorio
            var createdFactura = await _facturaRepository.CreateAsync(factura);

            return new FacturaResponseDTO
            {
                Id = createdFactura.Id,
                IdPago = createdFactura.IdPago,
                NumeroFactura = createdFactura.NumeroFactura,
                FechaEmision = createdFactura.FechaEmision,
                TotalPagar = createdFactura.TotalPagar,
                EstadoFactura = createdFactura.EstadoFactura
            };
        }


        public async Task<bool> UpdateAsync(int id, FacturaRequestDTO dto)
        {
            var factura = await _facturaRepository.GetByIdAsync(id);
            if (factura == null)
                return false;

            var type = typeof(Factura);
            // Actualizamos las propiedades utilizando reflection
            type.GetProperty("NumeroFactura", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .SetValue(factura, dto.NumeroFactura);
            type.GetProperty("TotalPagar", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .SetValue(factura, dto.TotalPagar);
            type.GetProperty("EstadoFactura", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .SetValue(factura, dto.EstadoFactura);
            // Si deseas actualizar la fecha de emisión (opcional), descomenta la siguiente línea:
            // type.GetProperty("FechaEmision", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).SetValue(factura, dto.FechaEmision);

            return await _facturaRepository.UpdateAsync(factura);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _facturaRepository.DeleteAsync(id);
        }
    }
}