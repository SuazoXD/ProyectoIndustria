
namespace Application.DTOs.Archivo
{
    public class ArchivoResponseDTO
    {
        public int Id { get; set; }
        public string NombreArchivo { get; set; }
        public string TipoArchivo { get; set; }
        public DateTime FechaSubida { get; set; }
        public string FuenteAlmacenamiento { get; set; }
        public string Metadatos { get; set; }
        public int IdUsuario { get; set; }
    }
}
