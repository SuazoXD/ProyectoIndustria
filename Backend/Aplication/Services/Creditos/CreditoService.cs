
using Aplication.DTOs.Creditos;
using Aplication.Interfaces.Creditos;
using Domain.Entities;

namespace Aplication.Services.Creditos
{
    public class CreditoService : ICreditoService
    {
        private readonly ICreditoRepository _creditoRepository;

        public CreditoService(ICreditoRepository creditoRepository)
        {
            _creditoRepository = creditoRepository;
        }

        public async Task<IEnumerable<CreditoResponseDTO>> GetAllAsync()
        {
            var creditos = await _creditoRepository.GetAllAsync();
            return creditos.Select(c => new CreditoResponseDTO
            {
                Id = c.Id,
                IdUsuario = c.IdUsuario,
                Cantidad = c.Cantidad,
                FechaAdquisicion = c.FechaAdquisicion
            });
        }

        public async Task<CreditoResponseDTO> GetByIdAsync(int id)
        {
            var credito = await _creditoRepository.GetByIdAsync(id);
            if (credito == null)
                return null;
            return new CreditoResponseDTO
            {
                Id = credito.Id,
                IdUsuario = credito.IdUsuario,
                Cantidad = credito.Cantidad,
                FechaAdquisicion = credito.FechaAdquisicion
            };
        }

        public async Task<CreditoResponseDTO> CreateAsync(CreditoRequestDTO dto)
        {
            var credito = new Credito(dto.IdUsuario, dto.Cantidad);
            var created = await _creditoRepository.CreateAsync(credito);
            return new CreditoResponseDTO
            {
                Id = created.Id,
                IdUsuario = created.IdUsuario,
                Cantidad = created.Cantidad,
                FechaAdquisicion = created.FechaAdquisicion
            };
        }

        public async Task<bool> UpdateAsync(int id, CreditoRequestDTO dto)
        {
            var credito = await _creditoRepository.GetByIdAsync(id);
            if (credito == null)
                return false;

           
            credito.GetType().GetProperty("Cantidad").SetValue(credito, dto.Cantidad);
            return await _creditoRepository.UpdateAsync(credito);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _creditoRepository.DeleteAsync(id);
        }
    }
}
