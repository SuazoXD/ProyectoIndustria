using Domain.AggregateRoots;
using Domain.Common;

namespace Domain.Entities
{
    public class ContenidoPremium : AggregateRoot
    {
        public string NombreContenido { get; private set; }
        public string TipoContenido { get; private set; }
        public int IdUsuario { get; private set; }
        public decimal Precio { get; private set; }

        public Usuario Usuario { get; set; }

        protected ContenidoPremium() { }

        public ContenidoPremium(string nombreContenido, string tipoContenido, int idUsuario, decimal precio)
        {
            NombreContenido = nombreContenido;
            TipoContenido = tipoContenido;
            IdUsuario = idUsuario;
            Precio = precio;
        }
    }
}
