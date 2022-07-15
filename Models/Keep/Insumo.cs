using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace netCoreNew.Models
{
    public class Insumo : IEntityBase
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        public double UnidadVenta { get; set; }
        public double PrecioDolar { get; set; }
        public string Observaciones { get; set; }        
        public bool Eliminado { get; set; }
        public string Etiquetas { get; set; }        
    }
}