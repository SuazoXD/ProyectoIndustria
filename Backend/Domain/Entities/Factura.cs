using Domain.Common;
using System;

namespace Domain.Entities
{
    public class Factura : AggregateRoot
    {
        public int Id { get; private set; }
        public int IdPago { get; private set; }
        public int NumeroFactura { get; private set; }
        public DateTime FechaEmision { get; private set; }
        public decimal TotalPagar { get; private set; }
        public string EstadoFactura { get; private set; }

        public Pago Pago { get; set; }

        protected Factura() { }

        public Factura(int idPago, int numeroFactura, decimal totalPagar, string estadoFactura = "pendiente")
        {
            IdPago = idPago;
            NumeroFactura = numeroFactura;
            TotalPagar = totalPagar;
            EstadoFactura = estadoFactura;
            FechaEmision = DateTime.UtcNow;
        }
    }
}
