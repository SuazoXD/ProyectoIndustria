using System;
using Domain.Common;

namespace Domain.Entities
{
    public class Archivo : AggregateRoot
    {
        public int Id { get; private set; }
        public string NombreArchivo { get; private set; }
        public string TipoArchivo { get; private set; }
        public DateTime FechaSubida { get; private set; }
        public string FuenteAlmacenamiento { get; private set; }
        public string Metadatos { get; private set; }
        public int IdUsuario { get; private set; }

        public Usuario Usuario { get; set; }

        public ICollection<Privacidad> Privacidades { get; set; }
        public ICollection<Favorito> Favoritos { get; set; }
        public ICollection<ArchivoListaReproduccion> ArchivosListas { get; set; }
    }
}
