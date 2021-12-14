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

    public class VerRecuentoVM
    {
        public int Id { get; set; }

        public string CreadoPor { get; set; }
        public string FechaCreacion { get; set; }
        public string FechaModificacion { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public string Total { get; set; }

        public IEnumerable<VerRecuentoDetalleVM> Detalles { get; set; }
    }

    public class VerRecuentoDetalleVM
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Precio { get; set; }
        public string Subtotal { get; set; }
        public string Cantidad { get; set; }
        public string Unidad { get; set; }
        public string Codigo { get; set; }
    }

    public class CodigoProveedorVM
    {
        public int Id { get; set; }
        public int IdArticulo { get; set; }
        public Articulo Articulo { get; set; }
        public int IdProveedor { get; set; }
        public Proveedor Proveedor { get; set; }
        public string Codigo { get; set; }
        public double PrecioProveedor { get; set; }
    }

    public class EditCodigoProveedorVM
    {
        public int IdArticulo { get; set; }
        public int IdProveedor { get; set; }
        public List<CodigoProveedorVM> Configuracion { get; set; }
    }
}