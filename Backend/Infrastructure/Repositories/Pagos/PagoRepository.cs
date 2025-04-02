using Aplication.DTOs.Pago;
using Aplication.Interfaces.Pagos;
using Domain.AggregateRoots;

namespace Application.Services.Pagos
{
    public class PagoService : IPagoService
    {
        private readonly IPagoRepository _pagoRepository;

        public PagoService(IPagoRepository pagoRepository)
        {
            _pagoRepository = pagoRepository;
        }

        public async Task<IEnumerable<PagoResponseDTO>> GetAllAsync()
        {
            var pagos = await _pagoRepository.GetAllAsync();
            return pagos.Select(p => new PagoResponseDTO
            {
                Id = p.Id,
                Monto = p.Monto,
                Estado = p.Estado,
                FechaPago = p.FechaPago,
                IdUsuario = p.IdUsuario,
                MetodoPago = p.MetodoPago
            });
        }

        public async Task<PagoResponseDTO> GetByIdAsync(int id)
        {
            var pago = await _pagoRepository.GetByIdAsync(id);
            if (pago == null) return null;
            return new PagoResponseDTO
            {
                Id = pago.Id,
                Monto = pago.Monto,
                Estado = pago.Estado,
                FechaPago = pago.FechaPago,
                IdUsuario = pago.IdUsuario,
                MetodoPago = pago.MetodoPago
            };
        }

        public async Task<PagoResponseDTO> CreateAsync(PagoRequestDTO dto)
        {
            // El constructor de Pago se define como: Pago(int idUsuario, int metodoPago, decimal monto, string estado, DateTime fechaPago)
            var pago = new Pago(dto.IdUsuario, dto.MetodoPago, dto.Monto, dto.Estado, dto.FechaPago);
            var created = await _pagoRepository.CreateAsync(pago);

            return new PagoResponseDTO
            {
                Id = created.Id,
                Monto = created.Monto,
                Estado = created.Estado,
                FechaPago = created.FechaPago,
                IdUsuario = created.IdUsuario,
                MetodoPago = created.MetodoPago
            };
        }


        public async Task<bool> UpdateAsync(int id, PagoRequestDTO dto)
        {
            var pago = await _pagoRepository.GetByIdAsync(id);
            if (pago == null) return false;

            // Llamar al método Update con el orden correcto:
            pago.Update(dto.Monto, dto.Estado, dto.MetodoPago, dto.FechaPago);

            return await _pagoRepository.UpdateAsync(pago);
        }



        public async Task<bool> DeleteAsync(int id)
        {
            return await _pagoRepository.DeleteAsync(id);
        }
    }
}
