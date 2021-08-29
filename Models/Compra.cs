using Microsoft.AspNetCore.Http;
using netCoreNew.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace netCoreNew.Models
{
    public class Compra
    {
        public Compra()
        {
            this.Detalles = new List<DetalleCompra>();
            this.Historiales = new List<HistorialCompra>();
            this.ItemsPartir = new List<PartirVM>();
        }

        public int Id { get; set; }
        public string Codigo { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaEntrega { get; set; }
        public DateTime? FechaUltimoEstado { get; set; }
        [NotMapped]
        public string FechaEntregaString { get; set; }
        public int IdTipoCompra { get; set; }
        public int? IdFormaPago { get; set; }
        public int IdCondicionPago { get; set; }
        public string Presupuesto1 { get; set; }
        public string Presupuesto2 { get; set; }
        public string Presupuesto3 { get; set; }
        public string Aprobador { get; set; }
        public string ObservacionPago { get; set; }
        public double Subtotal { get; set; }
        public double Iva { get; set; }
        public double Total => Subtotal + Iva;
        public int? IdProvincia { get; set; }
        [NotMapped]
        public IFormFile File1 { get; set; }
        [NotMapped]
        public IFormFile File2 { get; set; }
        [NotMapped]
        public IFormFile File3 { get; set; }

        public int IdCentroCostos { get; set; }
        [ForeignKey("IdCentroCostos")]
        public CentroCostos CentroCostos { get; set; }
        public int IdEstadoCompra { get; set; }
        [ForeignKey("IdEstadoCompra")]
        public EstadoCompra EstadoCompra { get; set; }
        public int IdUsuario { get; set; }
        [ForeignKey("IdUsuario")]
        public Usuario Solicitante { get; set; }
        public int? IdProveedor { get; set; }
        [ForeignKey("IdProveedor")]
        public Proveedor Proveedor { get; set; }

        [NotMapped]
        public string ItemsLoad { get; set; }
        [NotMapped]
        public DetalleCompra[] Items { get; set; }

        [NotMapped]
        public ICollection<PartirVM> ItemsPartir { get; set; }

        public ICollection<DetalleCompra> Detalles { get; set; }
        public ICollection<HistorialCompra> Historiales { get; set; }

        [NotMapped]
        public string FormaPago { get; set; }
        [NotMapped]
        public string CondicionPago { get; set; }

        public string LugarEntrega { get; set; }
        public double MontoEnvio { get; set; }

        public bool Ignorar { get; set; }
    }
}