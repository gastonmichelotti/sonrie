using Microsoft.AspNetCore.Http;
using netCoreNew.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace netCoreNew.Models
{
    public class HistorialRemito
    {
        public HistorialRemito()
        {

        }

        public int Id { get; set; }
        public string Propiedad { get; set; }
        public string Anterior { get; set; }
        public string Nuevo { get; set; }
        public string Responsable { get; set; }
        public DateTime Fecha { get; set; }
        
        public int IdRemito { get; set; }
        [ForeignKey("IdRemito")]
        public Remito Remito { get; set; }
    }
}