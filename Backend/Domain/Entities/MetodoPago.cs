using Domain.AggregateRoots;
using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class MetodoPago : AggregateRoot
    {
        public string NombreMetodo { get; private set; }

        // Nueva propiedad booleana para indicar si el método de pago está activo
        public bool Activo { get; private set; }

        public ICollection<Pago> Pagos { get; set; }

        protected MetodoPago()
        {
            Pagos = new List<Pago>();
        }

        public MetodoPago(string nombreMetodo, bool activo = true)
        {
            NombreMetodo = nombreMetodo;
            Activo = activo;
            Pagos = new List<Pago>();
        }

        // Método para actualizar el valor de la propiedad Activo, si es necesario
        public void ActualizarEstado(bool activo)
        {
            Activo = activo;
        }
    }
}
