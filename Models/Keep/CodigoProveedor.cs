using Microsoft.AspNetCore.Http;
using netCoreNew.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace netCoreNew.Models
{
    public class CodigoProveedor    
    {
        public CodigoProveedor()
        {
            Codigo = '-'.ToString();
        }

        public int Id { get; set; }
        public int IdArticulo { get; set; }
        [ForeignKey("IdArticulo")]

        public int IdProveedor { get; set; }
        [ForeignKey("IdProveedor")]

        public string Codigo { get; set; }

        public double PrecioProveedor { get; set; }
    }
}