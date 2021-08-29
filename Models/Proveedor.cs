using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace netCoreNew.Models
{
    public class Proveedor : IEntityBase
    {
        public int Id { get; set; }
        [Required]
        public string RazonSocial { get; set; }
        [Required]
        public string CUIT { get; set; }
        [Required]
        public string Alias { get; set; }
        public string Domicilio { get; set; }
        public string Localidad { get; set; }
        public string Contacto { get; set; }
        public string Telefono { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Notas { get; set; }
        public string Banco { get; set; }
        public string CBU { get; set; }
        public string RetencionGanancias { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaAlta { get; set; }

        public string DireccionFiscal { get; set; }
        public string LocalidadFiscal { get; set; }
        public string CPFiscal { get; set; }
        public string IIBB { get; set; }
        public string Etiquetas { get; set; }

        public int? IdCondicionIva { get; set; }
        public int IdCondicionPago { get; set; }

        public int? IdProvincia { get; set; }
        public int? IdProvinciaFiscal { get; set; }
    }
}