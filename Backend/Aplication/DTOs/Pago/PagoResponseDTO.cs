

using Aplication.DTOs.MetodoPago;
using Aplication.DTOs.Usuarios;

namespace Aplication.DTOs.Pago
{
    public class PagoResponseDTO
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int MetodoPago { get; set; }
        public decimal Monto { get; set; }
        public string Estado { get; set; }
        public DateTime FechaPago { get; set; }

        public UsuarioResponseDTO? Usuario { get; set; }

        public MetodoPagoResponseDTO? MetodoPagos { get; set; }
    }
}
