namespace Application.DTOs.Factura
{
    public class FacturaRequestDTO
    {
        public int IdPago { get; set; }
        public int NumeroFactura { get; set; }
        public decimal TotalPagar { get; set; }
        public string EstadoFactura { get; set; }  // Ej. "pendiente"
    }
}
