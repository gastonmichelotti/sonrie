using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace netCoreNew.Models
{
    public class CategoriaPrestacion : IEntityBase
    {
        public CategoriaPrestacion()
        {
            this.Detalles = new List<InsumoxCategoria>();
        }
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }       
        public string Observaciones { get; set; }        

        public bool Eliminado { get; set; }

        [NotMapped]
        public string ItemsLoad { get; set; }

        [NotMapped]
        public InsumoxCategoria[] Items { get; set; }
        public ICollection<Prestacion> Prestaciones { get; set; }
        public ICollection<InsumoxCategoria> Detalles { get; set; }

        //public string Marca { get; set; }
    }
}