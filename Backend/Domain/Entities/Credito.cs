using Domain.Common;

namespace Domain.Entities // o AggregateRoots si es necesario
{
    public class Credito : Entity
    {
        public int Id { get; private set; }
        public int IdUsuario { get; private set; }
        public int Cantidad { get; private set; }
        public DateTime FechaAdquisicion { get; private set; }

        public Usuario Usuario { get; set; }  // Esta propiedad debe existir

        protected Credito() { }

        public Credito(int idUsuario, int cantidad)
        {
            IdUsuario = idUsuario;
            Cantidad = cantidad;
            FechaAdquisicion = DateTime.UtcNow;
        }
    }
}
