using Microsoft.AspNetCore.Http;
using netCoreNew.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace netCoreNew.Models
{
    public class DetalleRecuento    
    {
        public DetalleRecuento()
        {

        }

        public int Id { get; set; }

        [NotMapped]
        public int IdDetalle { get; set; }

        public int Cantidad { get; set; }
        public double Precio { get; set; }
        public string UnidadMedida { get; set; }
        public string Codigo { get; set; }

        public double Subtotal => Cantidad * Precio;

        public int IdArticulo { get; set; }
        [ForeignKey("IdArticulo")]
        public Articulo Articulo { get; set; }
        public int IdRecuento { get; set; }
        [ForeignKey("IdRecuento")]
        public Recuento Recuento { get; set; }
    }
}