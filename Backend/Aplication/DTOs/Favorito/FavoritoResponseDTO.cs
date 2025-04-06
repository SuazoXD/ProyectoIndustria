using Aplication.DTOs.Archivo;
using Aplication.DTOs.Usuarios;

namespace Aplication.DTOs.Favorito
{
    public class FavoritoResponseDTO
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdArchivo { get; set; }
        public UsuarioResponseDTO? Usuario { get; set; }

        public ArchivoResponseDTO? Archivo { get; set; }
    }
}
