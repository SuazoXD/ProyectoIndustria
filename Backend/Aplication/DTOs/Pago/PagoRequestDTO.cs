namespace Application.DTOs.Pago
{
    public class PagoRequestDTO
    {
        public decimal Monto { get; set; }
        public string Estado { get; set; }
        public DateTime FechaPago { get; set; }
        public int IdUsuario { get; set; }
        public int MetodoPago { get; set; }
    }
}