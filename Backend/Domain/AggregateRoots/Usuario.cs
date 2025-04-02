using Domain.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.AggregateRoots
{
    public class Usuario : AggregateRoot
    {
        public int Id { get; private set; }
        public string NombreUsuario { get; private set; }
        public string Contrasenia { get; private set; }
        public string Correo { get; private set; }
        public int IdRol { get; private set; }
        public DateTime FechaRegistro { get; private set; }

        public Rol Rol { get; set; }
        public ICollection<Archivo> Archivos { get; set; }
        public ICollection<Pago> Pagos { get; set; }
        public ICollection<Credito> Creditos { get; set; }
        public ICollection<ContenidoPremium> ContenidosPremium { get; set; }
        public ICollection<Privacidad> Privacidades { get; set; }
        public ICollection<ListaDeReproduccion> ListasDeReproduccion { get; set; }
        public ICollection<Favorito> Favoritos { get; set; }

        protected Usuario() { }

        public Usuario(string nombreUsuario, string contrasenia, string correo, int idRol)
        {
            NombreUsuario = nombreUsuario;
            Contrasenia = contrasenia;
            Correo = correo;
            IdRol = idRol;
            FechaRegistro = DateTime.UtcNow;
        }
    }
}
