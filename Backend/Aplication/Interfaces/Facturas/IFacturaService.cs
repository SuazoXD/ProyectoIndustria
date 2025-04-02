using Aplication.DTOs.Factura;


namespace Aplication.Interfaces.Facturas
{
    public interface IFacturaService
    {
        Task<IEnumerable<FacturaResponseDTO>> GetAllAsync();
        Task<FacturaResponseDTO> GetByIdAsync(int id);
        Task<FacturaResponseDTO> CreateAsync(FacturaRequestDTO dto);
        Task<bool> UpdateAsync(int id, FacturaRequestDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
