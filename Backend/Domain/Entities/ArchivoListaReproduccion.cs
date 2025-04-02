using Domain.AggregateRoots;
using Domain.Common;

namespace Domain.Entities
{
    public class ArchivoListaReproduccion : AggregateRoot
    {
        public int IdLista { get; private set; }
        public int IdArchivo { get; private set; }

        // Navegación
        public ListaDeReproduccion ListaDeReproduccion { get; set; }
        public Archivo Archivo { get; set; }

        protected ArchivoListaReproduccion() { }

        public ArchivoListaReproduccion(int idLista, int idArchivo)
        {
            IdLista = idLista;
            IdArchivo = idArchivo;
        }
    }
}
