using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class MetodoPago : AggregateRoot
    {
        public string NombreMetodo { get; private set; }

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

        
        public void ActualizarEstado(bool activo)
        {
            Activo = activo;
        }
    }
}
