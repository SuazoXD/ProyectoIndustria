
using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Pago : AggregateRoot
    {
        public int Id { get; private set; }
        public int IdUsuario { get; private set; }
        public int MetodoPago { get; private set; } // FK a MetodoPago
        public decimal Monto { get; private set; }
        public string Estado { get; private set; }
        public DateTime FechaPago { get; private set; }

        // Propiedades de navegación
        public Usuario Usuario { get; set; }
        public MetodoPago MetodoPagoEntity { get; set; }
        public ICollection<Factura> Facturas { get; set; }

        // Constructor protegido para Entity Framework
        protected Pago()
        {
            Facturas = new List<Factura>();
        }

        // Constructor que toma 4 parámetros y asigna FechaPago con DateTime.UtcNow
        public Pago(int idUsuario, int metodoPago, decimal monto, string estado = "pendiente")
            : this(idUsuario, metodoPago, monto, estado, DateTime.UtcNow)
        { }

        // Constructor que toma 5 parámetros, incluyendo FechaPago
        public Pago(int idUsuario, int metodoPago, decimal monto, string estado, DateTime fechaPago)
        {
            IdUsuario = idUsuario;
            MetodoPago = metodoPago;
            Monto = monto;
            Estado = estado;
            FechaPago = fechaPago;
            Facturas = new List<Factura>();
        }

        // Método público para actualizar las propiedades de Pago sin acceder a los setters privados
        public void Update(decimal monto, string estado, int metodoPago, DateTime fechaPago)
        {
            Monto = monto;
            Estado = estado;
            MetodoPago = metodoPago;
            FechaPago = fechaPago;
        }
    }
}
