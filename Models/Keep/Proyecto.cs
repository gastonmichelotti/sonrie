using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace netCoreNew.Models
{
    public class Proyecto
    {
        public Proyecto()
        {

        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public int IdUsuario { get; set; }
        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; }
        public string ItemsLoad { get; set; }
        

    }
}