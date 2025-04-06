

using Aplication.DTOs.Usuarios;

namespace Aplication.DTOs.ContenidosPremium
{
    public class ContenidoPremiumResponseDTO
    {
        public int Id { get; set; }
        public string? NombreContenido { get; set; }
        public string? TipoContenido { get; set; }
        public int IdUsuario { get; set; }
        public decimal Precio { get; set; }

        public UsuarioResponseDTO? Usuario { get; set; }
    }
}