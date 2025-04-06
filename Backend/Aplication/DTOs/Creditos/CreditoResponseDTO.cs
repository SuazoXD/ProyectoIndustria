
using Aplication.DTOs.Usuarios;

namespace Aplication.DTOs.Creditos
{
    public class CreditoResponseDTO
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaAdquisicion { get; set; }

        public UsuarioResponseDTO? Usuario { get; set; }
    }
}
