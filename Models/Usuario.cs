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
            this.Compras = new List<Compra>();
        }

        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Telefono { get; set; }
        public int? IdProvincia { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime? UltimaConexion { get; set; }
        public DateTime? UltimaNotificacion { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [MinLength(5, ErrorMessage = "Al menos deben ser 5 letras")]
        [Required]
        public string Password { get; set; }
        public bool Eliminado { get; set; }
        public bool Activo { get; set; }

        public int IdRol { get; set; }
        [ForeignKey("IdRol")]
        public Rol Rol { get; set; }

        public ICollection<Compra> Compras { get; set; }
    }
}