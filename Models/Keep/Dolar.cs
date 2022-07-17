using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace netCoreNew.Models
{
    public class Dolar : IEntityBase
    {
        public int Id { get; set; }
        [Required]
        public double ValorDolar { get; set; }
        [Required]
        public DateTime Fecha{ get; set; }        
       
    }
}