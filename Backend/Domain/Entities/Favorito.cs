using Domain.AggregateRoots;
using Domain.Common;

namespace Domain.Entities
{
    public class Favorito : Entity
    {
        public int IdUsuario { get; private set; }
        public int IdArchivo { get; private set; }

      
        public Usuario Usuario { get; set; }
        public Archivo Archivo { get; set; }

        protected Favorito() { }

        public Favorito(int idUsuario, int idArchivo)
        {
            IdUsuario = idUsuario;
            IdArchivo = idArchivo;
        }
    }
}
