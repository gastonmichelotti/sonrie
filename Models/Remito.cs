using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace netCoreNew.Models
{
    public class Remito : IEntityBase
    {
        public Remito()
        {
            this.ItemsCompra = new DetalleCompra[0];
            this.Detalles = new List<DetalleRemito>();
            this.Historiales = new List<HistorialRemito>();
        }

        public int Id { get; set; }

        [MaxLength(4)]
        public string PuntoVenta { get; set; }
        [MaxLength(8)]
        public string NumeroRemito { get; set; }

        public string Codigo => PuntoVenta + "-" + NumeroRemito;

        public DateTime? FechaRecepcion { get; set; }
        [NotMapped]
        public string FechaRecepcionString { get; set; }

        public DateTime? FechaRemito { get; set; }
        [NotMapped]
        public string FechaRemitoString { get; set; }

        public string Comentario { get; set; }

        public string Adjunto { get; set; }
        [NotMapped]
        public IFormFile File1 { get; set; }

        public int IdCompra { get; set; }
        [ForeignKey("IdCompra")]
        public Compra Compra { get; set; }

        public int IdUsuario { get; set; }
        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; } //usuario receptor

        //public int IdProveedor { get; set; }
        //[ForeignKey("IdProveedor")]
        //public Proveedor Proveedor { get; set; }

        [NotMapped]
        public DetalleCompra[] ItemsCompra { get; set; }
        public ICollection<DetalleRemito> Detalles { get; set; }
        public ICollection<HistorialRemito> Historiales { get; set; }
    }
}