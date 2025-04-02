
namespace Aplication.DTOs.Factura
{
    public class FacturaResponseDTO
    {
        public int Id { get; set; }
        public int IdPago { get; set; }
        public int NumeroFactura { get; set; }
        public DateTime FechaEmision { get; set; }
        public decimal TotalPagar { get; set; }
        public string EstadoFactura { get; set; }
    }
}
