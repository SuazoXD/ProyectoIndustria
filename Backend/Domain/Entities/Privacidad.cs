using Domain.AggregateRoots;
using Domain.Common;

namespace Domain.Entities
{
    public class Privacidad : AggregateRoot
    {
        public int IdUsuario { get; private set; }
        public int IdArchivo { get; private set; }
        public int PermisoId { get; private set; }
        public bool Autodestruccion { get; private set; }
        public string DispositivosPermitidos { get; private set; }

        // Navegación
        public Usuario Usuario { get; set; }
        public Archivo Archivo { get; set; }
        public Permiso Permiso { get; set; }

        protected Privacidad() { }

        public Privacidad(int idUsuario, int idArchivo, int permisoId, bool autodestruccion, string dispositivosPermitidos)
        {
            IdUsuario = idUsuario;
            IdArchivo = idArchivo;
            PermisoId = permisoId;
            Autodestruccion = autodestruccion;
            DispositivosPermitidos = dispositivosPermitidos;
        }
    }
}
