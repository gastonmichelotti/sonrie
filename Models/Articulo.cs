using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace netCoreNew.Models
{
    public class Articulo : IEntityBase
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public string NombreCompleto => Nombre + " (" + UnidMedida + ")";
        public string Descripcion { get; set; }
        public string Marca { get; set; }
        public double Precio { get; set; }
        public string Observaciones { get; set; }
        public string UnidMedida { get; set; }
        public bool Activo { get; set; }
        public bool Eliminado { get; set; }
        public string Etiquetas { get; set; }

        //public string Marca { get; set; }
    }
}