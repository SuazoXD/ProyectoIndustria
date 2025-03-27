using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class ListaDeReproduccion : AggregateRoot
    {
        public int IdUsuario { get; private set; }
        public string NombreLista { get; private set; }
        public string Descripcion { get; private set; }

        // Navegación
        public Usuario Usuario { get; set; }
        public ICollection<ArchivoListaReproduccion> ArchivosListas { get; set; }

        protected ListaDeReproduccion()
        {
            ArchivosListas = new List<ArchivoListaReproduccion>();
        }

        public ListaDeReproduccion(int idUsuario, string nombreLista, string descripcion)
        {
            IdUsuario = idUsuario;
            NombreLista = nombreLista;
            Descripcion = descripcion;
            ArchivosListas = new List<ArchivoListaReproduccion>();
        }
    }
}

