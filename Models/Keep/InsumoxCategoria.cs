using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace netCoreNew.Models
{
    public class InsumoxCategoria : IEntityBase
    {
        
        public int Id { get; set; }
        [Required]
        
        public int IdCategoriaPrestacion { get; set; }
        [ForeignKey("IdCategoriaPrestacion")]
        public CategoriaPrestacion CategoriaPrestacion { get; set; }

        public int IdInsumo { get; set; }
        [ForeignKey("IdInsumo")]
        public Insumo Insumo { get; set; }

        public float Cantidad { get; set; }

    }
}