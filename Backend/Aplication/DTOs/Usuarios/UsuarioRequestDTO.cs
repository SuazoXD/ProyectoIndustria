namespace Aplication.DTOs.Usuarios
{
    public class UsuarioRequestDTO
    {
        public string NombreUsuario { get; set; }
        public string Contrasenia { get; set; }
        public string Correo { get; set; }
        public int IdRol { get; set; }
    }
}
