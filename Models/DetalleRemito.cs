using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace netCoreNew.Models
{
    public class DetalleRemito
    {
        public int Id { get; set; }

        public double Cantidad { get; set; }

        public int IdDetalleCompra { get; set; }
        [ForeignKey("IdDetalleCompra")]
        public DetalleCompra DetalleCompra { get; set; }

        public int IdRemito { get; set; }
        [ForeignKey("IdRemito")]
        public Remito Remito { get; set; }
    }
}