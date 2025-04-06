using Aplication.DTOs.Archivo;
using Aplication.DTOs.Permisos;
using Aplication.DTOs.Usuarios;

namespace Aplication.DTOs.Privacidades
{
    public class PrivacidadResponseDTO
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdArchivo { get; set; }
        public int PermisoId { get; set; }  
        public bool Autodestruccion { get; set; }
        public string DispositivosPermitidos { get; set; }

        public UsuarioResponseDTO? Usuario { get; set; }

        public ArchivoResponseDTO? Archivo { get; set; }
        public PermisoResponseDTO? Permiso { get; set; }
    }
}
