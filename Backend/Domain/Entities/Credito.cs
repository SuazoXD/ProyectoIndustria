using Domain.Common;

namespace Domain.Entities
{
    public class Credito : AggregateRoot
    {
        public int IdUsuario { get; private set; }
        public int Cantidad { get; private set; }
        public DateTime FechaAdquisicion { get; private set; }

        public Usuario Usuario { get; set; }

        protected Credito() { }

        public Credito (int idUsuario, int cantidad)
        {
            IdUsuario = idUsuario;
            Cantidad = cantidad;
            FechaAdquisicion = DateTime.UtcNow;
        }
    }
}
