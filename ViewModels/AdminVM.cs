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

    public class VerExportarVM
    {
        public VerExportarVM()
        {
            Items = new List<ItemSeleccionableVM>();
        }

        public int Id { get; set; }
        public string Instrucciones { get; set; }
        public string LugarEntrega { get; set; }
        public string Seleccionados { get; set; }
        public IEnumerable<ItemSeleccionableVM> Items { get; set; }
    }

    public class ItemSeleccionableVM
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Seleccionado { get; set; }
    }

    public class VerCompraVM
    {
        public int Id { get; set; }

        public int? Provincia { get; set; }
        public string Usuario { get; set; }
        public string Area { get; set; }
        public string Solicitado { get; set; }
        public string Plazo { get; set; }
        public string Proveedor { get; set; }
        public string CentroCostos { get; set; }
        public string TipoCompra { get; set; }
        public string Presupuesto1 { get; set; }
        public string Presupuesto2 { get; set; }
        public string Presupuesto3 { get; set; }
        public string Estado { get; set; }
        public string FormaPago { get; set; }
        public string CondicionPago { get; set; }
        public string Aprobador { get; set; }
        public string Obsrvaciones { get; set; }
        public bool VerObservaciones { get; set; }

        public string Total { get; set; }
        public string IVA { get; set; }
        public string Subtotal { get; set; }

        public bool Aprobar { get; set; }

       

        public IEnumerable<VerCompraArticulosVM> Articulos { get; set; }
    }

    public class VerCompraArticulosVM
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Precio { get; set; }
        public string Subtotal { get; set; }
        public string Cantidad { get; set; }
        public string Unidad { get; set; }
        public string Detalle { get; set; }
        public double PrecioNumber { get; set; }
    }

    //public class NotificacionesVM
    //{
    //    public List<NotificacionVM> Aprobadas { get; set; }
    //    public List<NotificacionVM> Rechazadas { get; set; }
    //    public List<NotificacionVM> Pendientes { get; set; }
    //}

    public class NotificacionVM
    {
        public string Fecha { get; set; }
        public string Nombre { get; set; }
        public string Icono { get; set; }
        public bool Leida { get; set; }
        public int IdCompra { get; set; }
    }

    public class PartirVM
    {
        public string Nombre { get; set; }
        public int Id { get; set; }
    }
}