using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace netCoreNew.Models
{
    public class CategoriaPestacion : IEntityBase
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }       
        public string Observaciones { get; set; }        
        public ICollection<Prestacion> Prestaciones { get; set; }

        //public string Marca { get; set; }
    }
}