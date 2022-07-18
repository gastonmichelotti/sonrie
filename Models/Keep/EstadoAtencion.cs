using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace netCoreNew.Models
{
    public class EstadoAtencion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Color { get; set; }
    }
}