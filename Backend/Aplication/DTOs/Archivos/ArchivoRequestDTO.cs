namespace Aplication.DTOs.Archivo
{
    public class ArchivoRequestDTO
    {
        public string NombreArchivo { get; set; }
        public string TipoArchivo { get; set; }  
        public string FuenteAlmacenamiento { get; set; }  
        public string Metadatos { get; set; }
        public int IdUsuario { get; set; }
    }
}