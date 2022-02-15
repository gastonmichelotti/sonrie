using Microsoft.AspNetCore.Http;
using netCoreNew.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace netCoreNew.Models
{
    public class Recuento
    {
        public Recuento()
        {
            this.Detalles = new List<DetalleRecuento>();
        }

        public int Id { get; set; }
        public bool Eliminado { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        //public string UnidadMedida { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string Etiquetas { get; set; }

        public int IdUsuario { get; set; }
        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; }

      

        public ICollection<DetalleRecuento> Detalles { get; set; }

        [NotMapped]
        public string ItemsLoad { get; set; }
        [NotMapped]
        public DetalleRecuento[] Items { get; set; }
       

        
    }
}