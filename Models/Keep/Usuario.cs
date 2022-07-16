using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace netCoreNew.Models
{
    public class Usuario : IEntityBase
    {
        public Usuario()
        {

        }

        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Telefono { get; set; }
        public DateTime FechaAlta { get; set; }
        
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [MinLength(5, ErrorMessage = "Al menos deben ser 5 letras")]
        [Required]
        public string Password { get; set; }
        public bool Activo { get; set; }

        public int IdRol { get; set; }
        [ForeignKey("IdRol")]
        public Rol Rol { get; set; }
    }
}