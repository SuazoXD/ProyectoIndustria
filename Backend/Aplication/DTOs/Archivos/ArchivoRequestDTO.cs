namespace Aplication.DTOs.Archivo
{
    public class ArchivoRequestDTO
    {
        public string NombreArchivo { get; set; }
        public string TipoArchivo { get; set; }  // 'musica', 'video', 'imagen'
        public string FuenteAlmacenamiento { get; set; }  // 'Google Drive', 'OneDrive'
        public string Metadatos { get; set; }
        public int IdUsuario { get; set; }
    }
}