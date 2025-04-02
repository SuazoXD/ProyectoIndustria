using Domain.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.AggregateRoots
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

        protected Archivo() { }

        public Archivo(string nombreArchivo, string tipoArchivo, string fuenteAlmacenamiento, string metadatos, int idUsuario)
        {
            NombreArchivo = nombreArchivo;
            TipoArchivo = tipoArchivo;
            FechaSubida = DateTime.UtcNow;
            FuenteAlmacenamiento = fuenteAlmacenamiento;
            Metadatos = metadatos;
            IdUsuario = idUsuario;
        }

        public void Update(string nombreArchivo, string tipoArchivo, string fuenteAlmacenamiento, string metadatos)
        {
            NombreArchivo = nombreArchivo;
            TipoArchivo = tipoArchivo;
            FuenteAlmacenamiento = fuenteAlmacenamiento;
            Metadatos = metadatos;
        }
    }
}
