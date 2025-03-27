using Domain.Common;

namespace Domain.Entities
{
    public class Permiso : AggregateRoot
    {
        public int Id { get; private set; }
        public string Descripcion { get; private set; }

        protected Permiso() { }

        public Permiso(string descripcion)
        {
            Descripcion = descripcion;
        }
        public void UpdateDescripcion(string descripcion)
        {
            Descripcion = descripcion;
        }
    }
}
