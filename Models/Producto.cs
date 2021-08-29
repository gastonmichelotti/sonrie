using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace netCoreNew.Models
{
    public class Producto : IEntityBase
    {
        public int Id { get; set; }
        [Required]
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }
        public string Foto { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
        public bool Activo { get; set; }
    }
}