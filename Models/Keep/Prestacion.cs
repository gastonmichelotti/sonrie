using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace netCoreNew.Models
{
    public class Prestacion : IEntityBase
    {
        public int Id { get; set; }
        [Required]
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Observaciones { get; set; }        
        public int IdCategoria { get; set; }     
        
    }
}