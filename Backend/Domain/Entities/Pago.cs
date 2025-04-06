using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Pago : Entity
    {
        public int Id { get; private set; }
        public int IdUsuario { get; private set; }
        public int MetodoPago { get; private set; }
        public decimal Monto { get; private set; }
        public string Estado { get; private set; }
        public DateTime FechaPago { get; private set; }

        public Usuario Usuario { get; set; }
        public MetodoPago MetodoPagoEntity { get; set; }
        public ICollection<Factura> Facturas { get; set; }

        protected Pago()
        {
            Facturas = new List<Factura>();
        }

        public Pago(int idUsuario, int metodoPago, decimal monto, string estado = "pendiente")
            : this(idUsuario, metodoPago, monto, estado, DateTime.UtcNow) { }

        public Pago(int idUsuario, int metodoPago, decimal monto, string estado, DateTime fechaPago)
        {
            IdUsuario = idUsuario;
            MetodoPago = metodoPago;
            Monto = monto;
            Estado = estado;
            FechaPago = fechaPago;
            Facturas = new List<Factura>();
        }

        public void Update(decimal monto, string estado, int metodoPago, DateTime fechaPago)
        {
            Monto = monto;
            Estado = estado;
            MetodoPago = metodoPago;
            FechaPago = fechaPago;
        }
    }
}
