using Aplication.DTOs.Archivo;
using Aplication.DTOs.ListaDeReproduccion;

namespace Aplication.DTOs.ArchivoListaReproduccion
{
    public class ArchivoListaReproduccionResponseDTO
    {
        public int Id { get; set; }
        public int IdLista { get; set; }
        public int IdArchivo { get; set; }
        public ListaDeReproduccionResponseDTO? Lista { get; set; }

        public ArchivoResponseDTO? Archivo { get; set; }
    }
}
