using Microsoft.AspNetCore.Http;
using netCoreNew.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace netCoreNew.ViewModels
{
    public class ExcelVM
    {
        public string Url { get; set; }
        [Display(Name = "Seleccione o arrastra tu Excel aqui")]
        public IFormFile File { get; set; }
    }

    //public class ExportarRecuentoVM
    //{
    //    public bool CodigoProveedor { get; set; }
    //    public int Coeficiente { get; set; }
    //    public int IdRecuento { get; set; }
    //    public string IdDetalles { get; set; }    //}

    
}