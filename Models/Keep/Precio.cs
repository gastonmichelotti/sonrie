using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace netCoreNew.Models
{
    public class Precio : IEntityBase
    {
        public int Id { get; set; }
        [Required]

        public int IdPrestacion { get; set; }
        [ForeignKey("IdPrestacion")]
        public Prestacion Prestacion { get; set; }

        public int  IdObraSocial { get; set; }
        [ForeignKey("IdObraSocial")]
        public ObraSocial ObraSocial { get; set; }

        public double PrecioPesos { get; set; }
        public double CoseguroPesos { get; set; }

        public DateTime Fecha { get; set; }
        public string Observaciones { get; set; }
    }
}