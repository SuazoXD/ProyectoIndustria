namespace Application.DTOs.Privacidades
{
    public class PrivacidadResponseDTO
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdArchivo { get; set; }
        public int PermisoId { get; set; }  // Agregado
        public bool Autodestruccion { get; set; }
        public string DispositivosPermitidos { get; set; }
    }
}
