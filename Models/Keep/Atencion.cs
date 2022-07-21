using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace netCoreNew.Models
{
    public class Atencion : IEntityBase
    {
        public Atencion()
        {
            Detalles = new List<PrestacionxAtencion>();
        }
        public int Id { get; set; }
        [Required]

        public DateTime Fecha { get; set; }

        public string FechaString { get; set; }

        public int IdPaciente { get; set; }
        [ForeignKey("IdPaciente")]
        public Paciente Paciente { get; set; }

        public int IdFormadePago { get; set; }        

        public int IdUsuario { get; set; }
        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; }

        public int IdEstadoAtencion { get; set; }
        [ForeignKey("IdEstadoAtencion")]
        public EstadoAtencion EstadoAtencion { get; set; }

        public double MontoEfectivo { get; set; }

        public double MontoOS { get; set; }   
    
        public bool Eliminado { get; set; }

        public string Observaciones { get; set; }

        [NotMapped]
        public string ItemsLoad { get; set; }

        [NotMapped]
        public PrestacionxAtencion[] Items { get; set; }

        public ICollection<PrestacionxAtencion> Detalles { get; set; }


    }
}