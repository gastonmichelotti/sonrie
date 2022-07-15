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
            this.Detalles = new List<PrestacionxAtencion>();
        }
        public int Id { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        public int IdPaciente { get; set; }
        [ForeignKey("IdPaciente")]
        public Paciente Paciente { get; set; }

        public int IdFormadePago { get; set; }
        public int IdEstadoAtencion { get; set; }

        public ICollection<PrestacionxAtencion> Detalles { get; set; }

    }
}