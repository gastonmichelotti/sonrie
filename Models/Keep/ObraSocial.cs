using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace netCoreNew.Models
{
    public class ObraSocial : IEntityBase
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        public int DemoraPago { get; set; }
        
        public string Observaciones { get; set; }
        
        public bool Activo { get; set; }
    }
}