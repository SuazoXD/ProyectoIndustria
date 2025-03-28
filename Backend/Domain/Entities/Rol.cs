using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Rol : AggregateRoot
    {
        public int Id { get; private set; }
        public string NombreRol { get; private set; }
        public string Descripcion { get; private set; }

        public ICollection<Usuario> Usuarios { get; set; }

        protected Rol()
        {
            Usuarios = new List<Usuario>();
        }

        public Rol(string nombreRol, string descripcion)
        {
            NombreRol = nombreRol;
            Descripcion = descripcion;
            Usuarios = new List<Usuario>();
        }
    }
}
