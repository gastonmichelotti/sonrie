using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace netCoreNew.Models
{
    public class Proveedor : IEntityBase
    {
        public int Id { get; set; }
        [Required]
        public string Alias { get; set; }
        public string Domicilio { get; set; }
        public string Localidad { get; set; }
        public string NombreContacto { get; set; }
        public string Telefono { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Observaciones { get; set; }
        public string Etiquetas { get; set; }
        public bool Activo { get; set; }
        public bool Eliminado { get; set; }
        public DateTime FechaAlta { get; set; }
    }
}