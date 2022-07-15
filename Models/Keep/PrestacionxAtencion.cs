using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace netCoreNew.Models
{
    public class PrestacionxAtencion : IEntityBase
    {
        public int Id { get; set; }
        [Required]
        public int IdPrestacion { get; set; }
        [ForeignKey("IdPrestacion")]
        public Prestacion Prestacion { get; set; }

        public int IdAtencion { get; set; }
        [ForeignKey("IdAtencion")]
        public Atencion Atencion { get; set; }

        public int IdPieza { get; set; }
        public string Caras { get; set; }

        public int IdObraSocial { get; set; }
        [ForeignKey("IdObraSocial")]
        public ObraSocial ObraSocial { get; set; }        

    }
}