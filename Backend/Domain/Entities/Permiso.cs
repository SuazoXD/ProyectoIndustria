using Domain.Common;

namespace Domain.Entities
{
    public class Permiso : Entity
    {
        public int Id { get; private set; }
        public string Descripcion { get; private set; }

        protected Permiso() { }

        public Permiso(string descripcion)
        {
            Descripcion = descripcion;
        }

        // Método público para actualizar la descripción
        public void UpdateDescripcion(string descripcion)
        {
            Descripcion = descripcion;
        }
    }
}
