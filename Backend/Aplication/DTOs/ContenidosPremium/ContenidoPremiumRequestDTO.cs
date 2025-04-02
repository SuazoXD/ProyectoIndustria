

namespace Aplication.DTOs.ContenidosPremium
{
    public class ContenidoPremiumRequestDTO
    {
        public string NombreContenido { get; set; }
        public string TipoContenido { get; set; }  // 'cancion', 'album', 'lista_de_reproduccion'
        public int IdUsuario { get; set; }
        public decimal Precio { get; set; }
    }
}
