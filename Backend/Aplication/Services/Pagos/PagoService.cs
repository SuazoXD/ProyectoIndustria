using Aplication.DTOs.MetodoPago;
using Aplication.DTOs.Pago;
using Aplication.DTOs.Usuarios;
using Aplication.Interfaces.Pagos;
using Domain.Entities;


namespace Aplication.Services.Pagos
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
                MetodoPago = p.MetodoPago,
                Usuario = p.Usuario != null ? new UsuarioResponseDTO
                {
                    Id = p.Usuario.Id,
                    NombreUsuario = p.Usuario.NombreUsuario,
                    Correo = p.Usuario.Correo,
                    FechaRegistro = p.Usuario.FechaRegistro,
                    IdRol = p.Usuario.IdRol
                } : null,
                MetodoPagos = p.MetodoPagoEntity != null ? new MetodoPagoResponseDTO
                {
                    Id = p.MetodoPagoEntity.Id,
                    NombreMetodo = p.MetodoPagoEntity.NombreMetodo,
                    Activo = p.MetodoPagoEntity.Activo
                } : null
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

        public async Task<IEnumerable<PagoResponseDTO>> GetAllByUserIdAsync(int userId)
        {
            var pagos = await _pagoRepository.GetAllAsync();
            return pagos
                .Where(p => p.IdUsuario == userId)
                .Select(p => new PagoResponseDTO
                {
                    Id = p.Id,
                    IdUsuario = p.IdUsuario,
                    Monto = p.Monto,
                    Estado = p.Estado,
                    FechaPago = p.FechaPago,
                    MetodoPago = p.MetodoPago
                });
        }

    }
}
