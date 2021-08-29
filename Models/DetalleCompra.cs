using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace netCoreNew.Models
{
    public class DetalleCompra
    {
        public int Id { get; set; }
        public double Cantidad { get; set; }
        public double Precio { get; set; }
        public double Subtotal => Precio * Cantidad;
        public double SubtotalIva => Precio * Cantidad * ((Iva ?? 0) / 100.0);
        public double Total => Subtotal + SubtotalIva;
        public double? Iva { get; set; }
        public int IdCompra { get; set; }
        [ForeignKey("IdCompra")]
        public Compra Compra { get; set; }
        public int IdArticulo { get; set; }
        [ForeignKey("IdArticulo")]
        public virtual Articulo Articulo { get; set; }

        [NotMapped]
        public double CantidadRemito { get; set; }
        [NotMapped]
        public double CantidadPendiente { get; set; }
    }
}