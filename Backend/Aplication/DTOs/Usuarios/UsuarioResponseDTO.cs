using System;

namespace Aplication.DTOs.Usuarios
{
    public class UsuarioResponseDTO
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Correo { get; set; }
        public int IdRol { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
