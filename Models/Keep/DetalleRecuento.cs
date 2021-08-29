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
        public int Cantidad { get; set; }

        public int IdArticulo { get; set; }
        [ForeignKey("IdArticulo")]
        public Articulo Articulo { get; set; }
        public int IdRecuento { get; set; }
        [ForeignKey("IdRecuento")]
        public Recuento Recuento { get; set; }
    }
}