using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace netCoreNew.Models
{
    public class Paciente : IEntityBase
    {
        public int Id { get; set; }
        [Required]
        public string Dni { get; set; }
        public string NombreApellido { get; set; }
        public string Telefono { get; set; }
        public string Mail { get; set; }

        public int  IdObraSocial { get; set; }
        [ForeignKey("IdObraSocial")]
        public ObraSocial ObraSocial { get; set; }

        public string OsPlan { get; set; }
        public string NumAfiliado { get; set; }
        public DateTime FechaAltaPaciente { get; set; }
        public string Observaciones { get; set; }
    }
}