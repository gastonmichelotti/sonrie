using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using netCoreNew.Business;
using netCoreNew.Enum;
using netCoreNew.Helpers;
using netCoreNew.Logic;
using netCoreNew.Models;
using netCoreNew.ViewModels;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace netCoreNew.Controllers
{
    [Authorize]
    public class AdminController : BaseController
    {
        private readonly IUsuarioService usuarioService;
        private readonly ICompraService compraService;
        private readonly IProveedorService proveedorService;
        private readonly IArticuloService articuloService;
        private readonly IRolService rolService;
        private readonly IEstadoCompraService estadoCompraService;
        private readonly IDetalleCompraService detalleCompraService;
        private readonly ICentroCostosService centroCostosService;
        private readonly INegocioService negocioService;
        private readonly IRemitoService remitoService;
        private readonly IDetalleRemitoService detalleRemitoService;
        private readonly IHistorialCompraService historialCompraService;
        private readonly IHistorialRemitoService historialRemitoService;
        private readonly IWebHostEnvironment hostingEnvironment;

        public AdminController(
            IUsuarioService usuarioService,
            ICompraService compraService,
            IProveedorService proveedorService,
            IArticuloService articuloService,
            IRolService rolService,
            IEstadoCompraService estadoCompraService,
            IDetalleCompraService detalleCompraService,
            ICentroCostosService centroCostosService,
            INegocioService negocioService,
            IRemitoService remitoService,
            IDetalleRemitoService detalleRemitoService,
            IHistorialCompraService historialCompraService,
            IHistorialRemitoService historialRemitoService,

            IWebHostEnvironment hostingEnvironment)
        {
            this.usuarioService = usuarioService;
            this.compraService = compraService;
            this.proveedorService = proveedorService;
            this.articuloService = articuloService;
            this.rolService = rolService;
            this.estadoCompraService = estadoCompraService;
            this.detalleCompraService = detalleCompraService;
            this.centroCostosService = centroCostosService;
            this.negocioService = negocioService;
            this.remitoService = remitoService;
            this.detalleRemitoService = detalleRemitoService;
            this.historialCompraService = historialCompraService;
            this.historialRemitoService = historialRemitoService;
            this.hostingEnvironment = hostingEnvironment;
        }

        #region USUARIOS
        [HttpGet]
        public IActionResult Usuarios()
        {
            ViewData["Title"] = "Usuarios";

            return View();
        }

        [HttpGet]
        public IActionResult CargarTablaUsuarios()
        {
            var final = CargarUsuarios(null);

            return Json(new { success = true, data = final });
        }

        private IEnumerable<object> CargarUsuarios(int? id)
        {
            return usuarioService.GetList(c => (id == null ? !c.Eliminado : c.Id == id), c => c.Rol)
                .Select(c => new
                {
                    id = c.Id,
                    nombre = c.Nombre,
                    email = c.Email,
                    telefono = c.Telefono,
                    rol = c.Rol.Nombre,
                    activo = c.Activo
                })
                .OrderBy(c => c.nombre);
        }

        [HttpGet]
        public IActionResult CreateUsuario(int id)
        {
            ViewBag.IdRol = new SelectList(rolService.GetAll(), "Id", "Nombre");

            return PartialView("_ModalUsuario", new Usuario
            {

            });
        }

        [HttpPost]
        public IActionResult CreateUsuario(Usuario model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = Valores.Incorrectos });
            }

            model.FechaAlta = CurrentDate;
            model.Activo = true;

            usuarioService.Add(model);

            var final = CargarUsuarios(model.Id).First();

            return Json(new { success = true, data = final, message = Valores.Creacion });
        }

        [HttpGet]
        public IActionResult EditUsuario(int id)
        {
            var result = usuarioService.GetById(id);

            ViewBag.IdRol = new SelectList(rolService.GetAll(), "Id", "Nombre", result.IdRol);

            return PartialView("_ModalUsuario", result);
        }

        [HttpPost]
        public IActionResult EditUsuario(Usuario model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = Valores.Incorrectos });
            }

            var usuario = usuarioService.GetById(model.Id);

            usuario.Nombre = model.Nombre;
            usuario.Telefono = model.Telefono;
            usuario.Email = model.Email;
            usuario.Password = model.Password;
            usuario.IdRol = model.IdRol;
            usuario.IdProvincia = model.IdProvincia;

            usuarioService.Edit(usuario);

            var final = CargarUsuarios(model.Id).First();

            return Json(new { success = true, data = final, message = Valores.Edicion });
        }

        public IActionResult ActivarUsuario(int id)
        {
            var model = usuarioService.GetById(id);

            model.Activo = !model.Activo;

            usuarioService.Edit(model);

            var final = CargarUsuarios(model.Id).First();

            return Json(new { success = true, data = final, message = Enum.Valores.Edicion });
        }

        public IActionResult EliminarUsuario(int id)
        {
            var model = usuarioService.GetById(id);

            model.Eliminado = true;
            model.Activo = false;
            model.Email = "eliminado_" + model.Email;

            usuarioService.Edit(model);

            return Json(new { success = true, message = Enum.Valores.Eliminacion });
        }
        #endregion

        #region MATERIALES
        [HttpGet]
        public IActionResult Materiales()
        {
            ViewData["Title"] = "Recepcion de materiales";

            ViewBag.IdEstado = new SelectList(estadoCompraService.GetAll(), "Nombre", "Nombre");
            ViewBag.IdProveedor = new SelectList(proveedorService.GetAll(), "Alias", "Alias");

            var desde = CurrentDate.Date.AddDays(-7);
            var hasta = CurrentDate.AddDays(1);

            if (User.IsInRole("Logistica"))
            {
                desde = CurrentDate.Date.AddMonths(-1);
            }

            ViewBag.Fecha = $"{desde.ToString("dd/MM/yyyy")} - {hasta.ToString("dd/MM/yyyy")}";

            return View();
        }

        [HttpGet]
        public IActionResult CargarTablaMateriales(string fecha = "")
        {
            var final = CargarMateriales(null, fecha);

            return Json(new { success = true, data = final });
        }

        private IEnumerable<object> CargarMateriales(int? id, string fecha = "")
        {
            var desde = CurrentDate.Date.AddDays(-7);
            var hasta = CurrentDate.AddDays(1);

            if (User.IsInRole("Logistica"))
            {
                desde = CurrentDate.Date.AddMonths(-1);
            }

            if (!string.IsNullOrEmpty(fecha))
            {
                var fechas = fecha.Replace(" ", "").Split('-');
                desde = DateTime.ParseExact(fechas[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                hasta = DateTime.ParseExact(fechas[1], "dd/MM/yyyy", CultureInfo.InvariantCulture).AddDays(1);
            }

            var usuario = usuarioService.GetByEmail(User.Identity.Name);

            return compraService.GetList(c => (id == null ?
                (
                    (c.IdEstadoCompra == (int)Valores.EstadoCompraEnm.Aprobada || c.IdEstadoCompra == (int)Valores.EstadoLogisticaEnm.RecibidaParcial || c.IdEstadoCompra == (int)Valores.EstadoLogisticaEnm.Recibida)
                    && c.IdProvincia == usuario.IdProvincia
                    && c.Fecha >= desde
                    && c.Fecha <= hasta
                ) : c.Id == id), c => c.Solicitante.Rol, c => c.EstadoCompra, c => c.Proveedor)
                .Select(c => new
                {
                    codigo = c.Codigo,
                    id = c.Id,
                    estado = $"<span class='text-{c.EstadoCompra.Color}'>{c.EstadoCompra.Nombre}</span>",
                    fecha = c.Fecha.ToString("dd/MM/yyyy"),
                    usuario = c.Solicitante.Nombre,
                    proveedor = c.Proveedor?.Alias,
                    pendiente = c.IdEstadoCompra == (int)Valores.EstadoCompraEnm.Aprobada || c.IdEstadoCompra == (int)Valores.EstadoLogisticaEnm.RecibidaParcial
                })
                .OrderByDescending(c => c.id);
        }
        #endregion

        #region REMITOS
        [HttpGet]
        public IActionResult Remitos()
        {
            ViewData["Title"] = "Remitos";

            ViewBag.IdUsuario = new SelectList(usuarioService.GetAll(), "Nombre", "Nombre");
            ViewBag.IdProveedor = new SelectList(proveedorService.GetAll(), "Alias", "Alias");

            return View();
        }

        [HttpGet]
        public IActionResult CargarTablaRemitos(string fecha = "")
        {
            var final = CargarRemitos(null, fecha);

            return Json(new { success = true, data = final });
        }

        private IEnumerable<object> CargarRemitos(int? id, string fecha = "")
        {
            var desde = CurrentDate.Date.AddDays(-7);
            var hasta = CurrentDate.AddDays(1);

            if (!string.IsNullOrEmpty(fecha))
            {
                var fechas = fecha.Replace(" ", "").Split('-');
                desde = DateTime.ParseExact(fechas[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                hasta = DateTime.ParseExact(fechas[1], "dd/MM/yyyy", CultureInfo.InvariantCulture).AddDays(1);
            }

            return remitoService.GetList(c => (id == null ?
                (
                    c.FechaRecepcion >= desde
                    && c.FechaRecepcion <= hasta
                ) : c.Id == id), c => c.Compra, c => c.Usuario, c => c.Compra.Proveedor)
                .Select(c => new
                {
                    codigo = c.Codigo,
                    id = c.Id,
                    fecha = c.FechaRecepcion?.ToString("dd/MM/yyyy"),
                    usuario = c.Usuario.Nombre,
                    proveedor = c.Compra.Proveedor?.Alias,
                    oc = c.Compra.Codigo
                })
                .OrderByDescending(c => c.id);
        }

        [HttpGet]
        public IActionResult VerRemitos(int id)
        {
            var result = remitoService.GetList(c => c.IdCompra == id, c => c.Usuario);

            return PartialView("_ModalRemitos", result);
        }

        [HttpGet]
        public IActionResult VerPendientes(int id)
        {
            //var remitos = remitoService.GetList(c => c.IdCompra == idCompra).Select(c => c.Id).ToArray();

            //var estado = (int)Valores.EstadoLogisticaEnm.Recibida;

            //if (!remitos.Any())
            //{
            //    estado = (int)Valores.EstadoLogisticaEnm.Aprobada;
            //}
            //else
            //{
            //    var items = detalleRemitoService.GetList(c => remitos.Contains(c.IdRemito));

            //    var itemsCompra = detalleCompraService.GetList(c => c.IdCompra == idCompra);

            //    foreach (var item in itemsCompra)
            //    {
            //        var cantidad = items.Where(c => c.IdDetalleCompra == item.Id).Sum(c => c.Cantidad);

            //        if (item.Cantidad > cantidad)
            //        {
            //            estado = (int)Valores.EstadoLogisticaEnm.RecibidaParcial;
            //        }
            //    }
            //}

            var compra = compraService.GetSingle(c => c.Id == id, c => c.Proveedor);

            var items = detalleCompraService.GetList(c => c.IdCompra == id, c => c.Articulo);

            foreach (var item in items)
            {
                var existe = detalleRemitoService.GetList(c => c.IdDetalleCompra == item.Id).Sum(c => c.Cantidad);

                item.CantidadPendiente = item.Cantidad - existe;
            }

            ViewBag.Title = compra.Codigo + " - " + compra.Proveedor?.Alias;

            return PartialView("_ModalPendiente", items);
        }

        [HttpGet]
        public IActionResult CreateRemito(int id)
        {
            var items = detalleCompraService.GetList(c => c.IdCompra == id, c => c.Articulo).ToArray();

            foreach (var item in items)
            {
                var existe = detalleRemitoService.GetList(c => c.IdDetalleCompra == item.Id).FirstOrDefault();

                item.Cantidad -= existe != null ? existe.Cantidad : 0d;
            }

            return PartialView("_ModalRemito", new Remito
            {
                FechaRecepcionString = CurrentDate.ToString("dd/MM/yyyy"),
                FechaRemitoString = CurrentDate.ToString("dd/MM/yyyy"),
                IdCompra = id,
                ItemsCompra = items
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateRemitoAsync(Remito model)
        {
            ModelState.Remove("Id");

            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = Valores.Incorrectos });
            }

            if (ValidarRemito(model))
            {
                return Json(new { success = false, message = "Remito repetido" });
            }

            if (model.File1 != null && model.File1.Length > 0)
            {
                var folder = "files";
                var uploads = Path.Combine(hostingEnvironment.WebRootPath, folder);
                var extension = Path.GetExtension(model.File1.FileName);
                var nombre = Path.GetRandomFileName() + extension;
                var filePath = Path.Combine(uploads, nombre);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.File1.CopyToAsync(stream);
                }
                model.Adjunto = Path.Combine("../../" + folder, nombre);
            }

            model.FechaRecepcion = DateTime.ParseExact(model.FechaRecepcionString, "dd/MM/yyyy", null);
            model.FechaRemito = DateTime.ParseExact(model.FechaRecepcionString, "dd/MM/yyyy", null);
            model.IdUsuario = usuarioService.GetByEmail(User.Identity.Name).Id;

            foreach (var item in model.ItemsCompra)
            {
                model.Detalles.Add(new DetalleRemito
                {
                    Cantidad = item.CantidadRemito,
                    IdDetalleCompra = item.Id
                });
            }

            //model.ItemsCompra = null;
            model.Id = 0;

            remitoService.Add(model);

            //var items = detalleRemitoService.GetList(c => c.IdRemito == model.Id);

            //var estado = (int)Valores.EstadoLogisticaEnm.Recibida;

            //foreach (var item in items)
            //{
            //    if (item.Cantidad != detalleCompraService.GetById(item.IdDetalleCompra).Cantidad)
            //    {
            //        estado = (int)Valores.EstadoLogisticaEnm.RecibidaParcial;
            //    }
            //}

            //var compra = compraService.GetById(model.IdCompra);

            //compra.IdEstadoCompra = estado;

            //compraService.Edit(compra);

            ActualizarRemito(model.Id, model.IdCompra);

            var final = CargarMateriales(model.IdCompra).First();

            return Json(new { success = true, data = final, message = Valores.Creacion });
        }

        [HttpGet]
        public IActionResult EditRemito(int id)
        {
            var result = remitoService.GetById(id);

            result.FechaRecepcionString = result.FechaRecepcion?.ToString("dd/MM/yyyy");
            result.FechaRemitoString = result.FechaRemito?.ToString("dd/MM/yyyy");
            result.ItemsCompra = detalleCompraService.GetList(c => c.IdCompra == result.IdCompra, c => c.Articulo).ToArray();

            var items = detalleRemitoService.GetList(c => c.IdRemito == id);

            foreach (var item in result.ItemsCompra)
            {
                var existe = items.FirstOrDefault(c => c.IdDetalleCompra == item.Id);

                item.CantidadRemito = existe != null ? existe.Cantidad : 0d;
            }

            return PartialView("_ModalRemito", result);
        }

        [HttpPost]
        public async Task<IActionResult> EditRemitoAsync(Remito model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = Valores.Incorrectos });
            }

            if (ValidarRemito(model))
            {
                return Json(new { success = false, message = "Remito repetido" });
            }

            var remito = remitoService.GetById(model.Id);

            remito.FechaRecepcion = DateTime.ParseExact(model.FechaRecepcionString, "dd/MM/yyyy", null);
            remito.FechaRemito = DateTime.ParseExact(model.FechaRemitoString, "dd/MM/yyyy", null);
            remito.NumeroRemito = model.NumeroRemito;
            remito.PuntoVenta = model.PuntoVenta;
            remito.Comentario = model.Comentario;

            if (model.File1 != null && model.File1.Length > 0)
            {
                var folder = "files";
                var uploads = Path.Combine(hostingEnvironment.WebRootPath, folder);
                var extension = Path.GetExtension(model.File1.FileName);
                var nombre = Path.GetRandomFileName() + extension;
                var filePath = Path.Combine(uploads, nombre);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.File1.CopyToAsync(stream);
                }
                remito.Adjunto = Path.Combine("../../" + folder, nombre);
            }

            var items = detalleRemitoService.GetList(c => c.IdRemito == model.Id);

            foreach (var item in model.ItemsCompra)
            {
                var existe = items.FirstOrDefault(c => c.IdDetalleCompra == item.Id);

                if (existe != null)
                {
                    existe.Cantidad = item.CantidadRemito;
                }
                else
                {
                    remito.Detalles.Add(new DetalleRemito
                    {
                        Cantidad = item.CantidadRemito,
                        IdDetalleCompra = item.Id
                    });
                }
            }

            remitoService.Edit(remito);

            ActualizarRemito(model.Id, model.IdCompra);

            var request = Request.Path;

            var final = CargarRemitos(model.Id).First();

            return Json(new { success = true, data = final, message = Valores.Edicion });
        }

        private bool ValidarRemito(Remito remito)
        {
            var proveedor = compraService.GetById(remito.IdCompra).IdProveedor;

            return remitoService.GetList(c => c.Compra.IdProveedor == proveedor
            && c.PuntoVenta == remito.PuntoVenta
            && c.NumeroRemito == remito.NumeroRemito
            && c.Id != remito.Id, c => c.Compra).Any();
        }

        public IActionResult EliminarRemito(int id)
        {
            var model = detalleRemitoService.GetList(c => c.IdRemito == id);

            detalleRemitoService.DeleteRange(model.ToArray());

            var remito = remitoService.GetById(id);

            remitoService.Delete(remito);

            ActualizarRemito(remito.Id, remito.IdCompra);

            return Json(new { success = true, message = Enum.Valores.Eliminacion });
        }

        private void ActualizarRemito(int idRemito, int idCompra)
        {
            var remitos = remitoService.GetList(c => c.IdCompra == idCompra).Select(c => c.Id).ToArray();

            var estado = (int)Valores.EstadoLogisticaEnm.Recibida;

            if (!remitos.Any())
            {
                estado = (int)Valores.EstadoLogisticaEnm.Aprobada;
            }
            else
            {
                var items = detalleRemitoService.GetList(c => remitos.Contains(c.IdRemito));

                var itemsCompra = detalleCompraService.GetList(c => c.IdCompra == idCompra);

                foreach (var item in itemsCompra)
                {
                    var cantidad = items.Where(c => c.IdDetalleCompra == item.Id).Sum(c => c.Cantidad);

                    if (item.Cantidad > cantidad)
                    {
                        estado = (int)Valores.EstadoLogisticaEnm.RecibidaParcial;
                    }
                }
            }

            //if (!items.Any())
            //{
            //    estado = (int)Valores.EstadoLogisticaEnm.Aprobada;
            //}
            //else
            //{
            //    foreach (var item in items)
            //    {
            //        if (item.Cantidad < detalleCompraService.GetById(item.IdDetalleCompra).Cantidad)
            //        {
            //            estado = (int)Valores.EstadoLogisticaEnm.RecibidaParcial;
            //        }
            //    }
            //}

            var compra = compraService.GetById(idCompra);

            compra.IdEstadoCompra = estado;

            compraService.Edit(compra);
        }

        [HttpGet]
        public IActionResult HistorialRemito(int id)
        {
            var historiales = historialRemitoService.GetList(c => c.IdRemito == id);

            return PartialView("_ModalHistorialRemito", historiales);
        }
        #endregion

        #region COMPRAS
        [HttpGet]
        public IActionResult Compras()
        {
            ViewData["Title"] = "Ordenes de compra";

            var filter = User.IsInRole("Gerencia General") || User.IsInRole("Gerencia Operaciones") || User.IsInRole("Administrador")
                ? estadoCompraService.GetById((int)Valores.EstadoCompraEnm.PendienteAprobacion).Nombre
                : "";

            ViewBag.IdEstado = new SelectList(estadoCompraService.GetAll(), "Nombre", "Nombre", filter);
            ViewBag.IdArea = new SelectList(rolService.GetAll(), "Nombre", "Nombre");
            ViewBag.IdProveedor = new SelectList(proveedorService.GetAll(), "Alias", "Alias");

            //if (User.IsInRole("Gerencia General") || User.IsInRole("Gerencia Operaciones") || User.IsInRole("Administrador"))
            //{
            //    var hasta = CurrentDate.Date;
            //    var desde = CurrentDate.Date.AddDays(-7);

            //    ViewBag.Fecha = $"{desde.ToString("dd/MM/yyyy")} - {hasta.ToString("dd/MM/yyyy")}";
            //}

            var desde = CurrentDate.Date;
            var hasta = CurrentDate.AddDays(1);

            if (User.IsInRole("Gerencia General") || User.IsInRole("Gerencia Operaciones") || User.IsInRole("Administrador"))
            {
                desde = CurrentDate.Date.AddDays(-7);
            }

            if (User.IsInRole("Compras"))
            {
                desde = CurrentDate.Date.AddMonths(-1);
            }

            ViewBag.Fecha = $"{desde.ToString("dd/MM/yyyy")} - {hasta.ToString("dd/MM/yyyy")}";

            return View();
        }

        [HttpGet]
        public IActionResult CargarTablaCompras(string fecha = "")
        {
            var final = CargarCompras(null, fecha);

            return Json(new { success = true, data = final });
        }

        private IEnumerable<object> CargarCompras(int? id, string fecha = "")
        {
            var desde = CurrentDate.Date;
            var hasta = CurrentDate.AddDays(1);

            if (User.IsInRole("Gerencia General") || User.IsInRole("Gerencia Operaciones") || User.IsInRole("Administrador"))
            {
                desde = CurrentDate.Date.AddDays(-7);
            }

            var esCompras = User.IsInRole("Compras");

            if (esCompras)
            {
                desde = CurrentDate.Date.AddMonths(-1);
            }

            if (!string.IsNullOrEmpty(fecha))
            {
                var fechas = fecha.Replace(" ", "").Split('-');
                desde = DateTime.ParseExact(fechas[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                hasta = DateTime.ParseExact(fechas[1], "dd/MM/yyyy", CultureInfo.InvariantCulture).AddDays(1);
            }

            var todas = User.IsInRole("Administrador") || User.IsInRole("Gerencia General") || User.IsInRole("Gerencia Operaciones") || User.IsInRole("Compras") || User.IsInRole("Programacion de Produccion") || User.IsInRole("Logistica");
            var aprobar = User.IsInRole("Administrador") || User.IsInRole("Gerencia General") || User.IsInRole("Gerencia Operaciones");
            var comprador = User.IsInRole("Administrador") || User.IsInRole("Compras");

            var user = usuarioService.GetByEmail(User.Identity.Name);
            var rol = user.IdRol;

            return compraService.GetList(c => (id == null ?
                (
                    c.Fecha >= desde
                    && c.Fecha <= hasta
                    && (todas ? true : c.Solicitante.IdRol == rol)
                    && (User.IsInRole("Gerencia General") ? c.IdTipoCompra == (int)Valores.TipoCompraEnum.General : true)
                    && (User.IsInRole("Gerencia Operaciones") ? c.IdTipoCompra == (int)Valores.TipoCompraEnum.Productiva : true)
                ) : c.Id == id), c => c.Solicitante.Rol, c => c.EstadoCompra, c => c.Proveedor)
                .Select(c => new
                {
                    codigo = c.Codigo,
                    id = c.Id,
                    estado = $"<span class='text-{c.EstadoCompra.Color}'>{c.EstadoCompra.Nombre}</span>",
                    fecha = c.Fecha.ToString("dd/MM/yyyy"),
                    fechaEntrega = c.FechaEntrega.ToString("dd/MM/yyyy"),
                    usuario = c.Solicitante.Nombre,
                    area = c.Solicitante.Rol.Nombre,
                    proveedor = c.Proveedor?.Alias,
                    cancelar = todas && c.IdEstadoCompra == (int)Valores.EstadoCompraEnm.Abierta,
                    aprobar = aprobar && c.IdEstadoCompra == (int)Valores.EstadoCompraEnm.PendienteAprobacion,
                    ignorar = aprobar,
                    ignorarvalue = c.Ignorar ? "checked" : "",
                    formulario = c.IdEstadoCompra == (int)Valores.EstadoCompraEnm.Aprobada,
                    total = c.Total.ToString("C0"),
                    exportar = comprador && c.IdEstadoCompra == (int)Valores.EstadoCompraEnm.Abierta,
                    editar = esCompras ? c.IdProvincia == user.IdProvincia : true
                })
                .OrderByDescending(c => c.id);
        }

        [HttpGet]
        public IActionResult CreateCompra()
        {
            ViewBag.IdProveedor = new SelectList(proveedorService.GetAll(), "Id", "Alias");
            ViewBag.IdArticulo = new SelectList(articuloService.GetAll(), "Id", "NombreCompleto");
            //ViewBag.IdCentroCostos = new SelectList(centroCostosService.GetAll(), "Id", "Nombre");
            ViewBag.IdFormaPago = new SelectList(Valores.CondicionPagoEnum);
            ViewBag.Iva = new SelectList(Valores.IvaEnum);

            return PartialView("_ModalCompra", new Compra
            {
                FechaEntregaString = CurrentDate.ToString("dd/MM/yyyy")
            });
        }

        [HttpPost]
        public IActionResult CreateCompra(Compra model)
        {
            ModelState.Remove("Id");

            ModelState.Remove("IdProveedor");

            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = Valores.Incorrectos });
            }

            if (User.IsInRole("Logistica") || User.IsInRole("Compras"))
            {

            }
            else
            {
                if (model.IdTipoCompra == (int)netCoreNew.Enum.Valores.TipoCompraEnum.Productiva)
                {
                    return Json(new { success = false, message = "Solo puedes crear ordenes de compra del tipo Generales" });
                }
            }

            if (model.Items == null || !model.Items.Any())
            {
                return Json(new { success = false, message = "Debe seleccionar al menos un articulo" });
            }

            var usuario = usuarioService.GetSingle(c => c.Email == User.Identity.Name);

            //model.FechaEntrega = DateTime.ParseExact(model.FechaEntregaString.Substring(0, 15), "ddd MMM dd yyyy", CultureInfo.InvariantCulture);
            model.FechaEntrega = DateTime.ParseExact(model.FechaEntregaString, "dd/MM/yyyy", null);
            model.Fecha = CurrentDate;
            model.IdUsuario = usuario.Id;
            model.IdEstadoCompra = (int)Valores.EstadoCompraEnm.Abierta;
            model.IdProvincia = usuario.IdProvincia;

            var iva = Valores.IvaEnum[2];

            if (model.IdProveedor != null)
            {
                var proveedor = proveedorService.GetById(model.IdProveedor.Value);

                model.IdFormaPago = proveedor.IdCondicionPago;

                switch (proveedor.IdCondicionIva)
                {
                    case (int)Valores.CondicionFrenteIvaEnum.ResponsableInscripto:
                        iva = Valores.IvaEnum[0];
                        break;
                    case (int)Valores.CondicionFrenteIvaEnum.Excento:
                        iva = Valores.IvaEnum[2];
                        break;
                    case (int)Valores.CondicionFrenteIvaEnum.Monotributista:
                        iva = Valores.IvaEnum[2];
                        break;
                    default:
                        break;
                }
            }

            foreach (var item in model.Items)
            {
                model.Detalles.Add(new DetalleCompra
                {
                    IdArticulo = item.IdArticulo,
                    Cantidad = item.Cantidad,
                    Precio = 0,
                    Iva = iva
                });
            }

            if (model.IdProvincia == (int)Valores.ProvinciaEnum.Cordoba)
            {
                var fix = 1300;
                model.Codigo = $"01-{fix + compraService.GetList(c => c.IdProvincia == (int)Valores.ProvinciaEnum.Cordoba).Count()}";
            }
            else
            {
                var fix = compraService.GetList(c => c.IdProvincia == (int)Valores.ProvinciaEnum.BuenosAires).Count().ToString();
                for (int i = fix.Count(); i < 4; i++)
                {
                    fix = "0" + fix;
                }
                model.Codigo = $"02-{fix}";
            }

            model.Subtotal = model.Detalles.Sum(c => c.Subtotal);
            model.Iva = model.Detalles.Sum(c => c.SubtotalIva);

            compraService.Add(model);

            var final = CargarCompras(model.Id).First();

            return Json(new { success = true, data = final, message = Valores.Creacion, editar = User.IsInRole("Compras"), id = model.Id });
        }

        [HttpGet]
        public IActionResult EditCompra(int id)
        {
            if (!User.IsInRole("Administrador") && !User.IsInRole("Compras"))
            {
                throw new Exception("Solo usuarios del area COMPRAS pueden editar");
            }

            var result = compraService.GetById(id);

            if (result.IdEstadoCompra == (int)Valores.EstadoCompraEnm.Aprobada)
            {
                if (!User.IsInRole("Compras"))
                {
                    throw new Exception("No se pueden editar compras APROBADAS");
                }
            }

            //if (User.IsInRole("Compras"))
            //{
            //    if (result.IdEstadoCompra != (int)Valores.EstadoCompraEnm.Aprobada)
            //    {
            //        throw new Exception("No se pueden editar compras APROBADAS");
            //    }
            //}
            //else
            //{
            //    if (result.IdEstadoCompra != (int)Valores.EstadoCompraEnm.Abierta)
            //    {
            //        throw new Exception("Solo ordenes en estado ABIERTA pueden ser editadas");
            //    }
            //}

            result.FechaEntregaString = result.FechaEntrega.ToString("dd/MM/yyyy");

            ViewBag.IdProveedor = new SelectList(proveedorService.GetAll(), "Id", "Alias", result.IdProveedor);
            ViewBag.IdArticulo = new SelectList(articuloService.GetAll(), "Id", "NombreCompleto");
            //ViewBag.IdEstadoCompra = new SelectList(estadoCompraService.GetAll(), "Id", "Nombre", result.IdEstadoCompra);
            //ViewBag.IdCentroCostos = new SelectList(centroCostosService.GetAll(), "Id", "Nombre", result.IdCentroCostos);
            ViewBag.IdFormaPago = new SelectList(Valores.CondicionPagoEnum, result.IdFormaPago);
            ViewBag.Iva = new SelectList(Valores.IvaEnum);

            var items = detalleCompraService.GetList(c => c.IdCompra == id).Select(c => new
            {
                Nombre = c.IdArticulo + "+" + c.Cantidad + "+" + c.Iva + "+" + c.Precio
            });

            result.ItemsLoad = string.Join(';', items.Select(c => c.Nombre));

            result.ItemsPartir = detalleCompraService.GetList(c => c.IdCompra == id, c => c.Articulo).Select(c => new PartirVM
            {
                Id = c.Id,
                Nombre = c.Articulo.Nombre
            }).ToList();

            return PartialView("_ModalCompra", result);
        }

        [HttpPost]
        public async Task<IActionResult> EditCompraAsync(Compra model)
        {
            ModelState.Remove("Iva");

            if (model.IdProveedor == null)
            {
                return Json(new { success = false, message = "Debe seleccionar un proveedor" });
            }

            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = Valores.Incorrectos });
            }

            if (model.Items == null || !model.Items.Any())
            {
                return Json(new { success = false, message = "Debe seleccionar al menos un articulo" });
            }

            var compra = compraService.GetById(model.Id);

            if (compra.Presupuesto1 == null && model.File1 == null)
            {
                return Json(new { success = false, message = "Debe cargar un presupuesto" });
            }

            compra.IdTipoCompra = model.IdTipoCompra;
            //compra.IdCentroCostos = model.IdCentroCostos;
            compra.IdCondicionPago = model.IdCondicionPago;
            compra.IdFormaPago = model.IdFormaPago;
            //compra.FechaEntrega = DateTime.ParseExact(model.FechaEntregaString.Substring(0, 15), "ddd MMM dd yyyy", CultureInfo.InvariantCulture);
            compra.FechaEntrega = DateTime.ParseExact(model.FechaEntregaString, "dd/MM/yyyy", null);
            compra.IdProveedor = model.IdProveedor;
            compra.ObservacionPago = model.ObservacionPago;

            if (model.File1 != null && model.File1.Length > 0)
            {
                var folder = "files";
                var uploads = Path.Combine(hostingEnvironment.WebRootPath, folder);
                var extension = Path.GetExtension(model.File1.FileName);
                var nombre = Path.GetRandomFileName() + extension;
                var filePath = Path.Combine(uploads, nombre);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.File1.CopyToAsync(stream);
                }
                compra.Presupuesto1 = Path.Combine("../../" + folder, nombre);
            }

            if (model.File2 != null && model.File2.Length > 0)
            {
                var folder = "files";
                var uploads = Path.Combine(hostingEnvironment.WebRootPath, folder);
                var extension = Path.GetExtension(model.File2.FileName);
                var nombre = Path.GetRandomFileName() + extension;
                var filePath = Path.Combine(uploads, nombre);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.File2.CopyToAsync(stream);
                }
                compra.Presupuesto2 = Path.Combine("../../" + folder, nombre);
            }

            if (model.File3 != null && model.File3.Length > 0)
            {
                var folder = "files";
                var uploads = Path.Combine(hostingEnvironment.WebRootPath, folder);
                var extension = Path.GetExtension(model.File3.FileName);
                var nombre = Path.GetRandomFileName() + extension;
                var filePath = Path.Combine(uploads, nombre);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.File3.CopyToAsync(stream);
                }
                compra.Presupuesto3 = Path.Combine("../../" + folder, nombre);
            }

            detalleCompraService.DeleteRange(detalleCompraService.GetList(c => c.IdCompra == model.Id).ToArray());

            var iva = Valores.IvaEnum[2];

            var proveedor = proveedorService.GetById(model.IdProveedor.Value);

            //model.IdFormaPago = proveedor.IdCondicionPago;

            switch (proveedor.IdCondicionIva)
            {
                case (int)Valores.CondicionFrenteIvaEnum.ResponsableInscripto:
                    iva = Valores.IvaEnum[0];
                    break;
                case (int)Valores.CondicionFrenteIvaEnum.Excento:
                    iva = Valores.IvaEnum[2];
                    break;
                case (int)Valores.CondicionFrenteIvaEnum.Monotributista:
                    iva = Valores.IvaEnum[2];
                    break;
                default:
                    break;
            }

            foreach (var item in model.Items)
            {
                compra.Detalles.Add(new DetalleCompra
                {
                    IdArticulo = item.IdArticulo,
                    Cantidad = item.Cantidad,
                    Precio = item.Precio,
                    Iva = item.Iva == -1 ? iva : item.Iva
                });
            }

            if (compra.IdEstadoCompra == (int)Valores.EstadoCompraEnm.Abierta)
            {
                if (compra.IdTipoCompra == (int)Valores.TipoCompraEnum.General)
                {
                    compra.IdEstadoCompra = (int)Valores.EstadoCompraEnm.PendienteAprobacion;

                    var solicitante = usuarioService.GetList(c => c.Id == compra.IdUsuario, c => c.Rol).First().Rol.Nombre;

                    if (solicitante == "Logistica" || solicitante == "Programacion de Produccion")
                    {
                        compra.Aprobador = "Gerencia Generals";
                    }
                    else
                    {
                        compra.Aprobador = "Gerencia Operaciones";
                    }
                }
                else
                {
                    var total = compra.Detalles.Sum(c => c.Subtotal);

                    var negocio = negocioService.GetAll().First();

                    if (total > negocio.MontoMinimo)
                    {
                        compra.Aprobador = "Gerencia Operaciones";
                        compra.IdEstadoCompra = (int)Valores.EstadoCompraEnm.PendienteAprobacion;
                    }
                    else
                    {
                        foreach (var item in compra.Detalles)
                        {
                            var ultima = detalleCompraService.GetList(c => c.IdArticulo == item.IdArticulo
                            && c.Compra.IdProvincia == compra.IdProvincia
                            && c.IdCompra != model.Id
                            && c.Compra.IdEstadoCompra == (int)Valores.EstadoCompraEnm.Aprobada
                            && !c.Compra.Ignorar, c => c.Compra)
                            .LastOrDefault()?.Precio;

                            if (ultima != null)
                            {
                                var calculo = ultima * (1 + (negocio.MontoDiferencia / 100.0));

                                if (item.Precio > calculo)
                                {
                                    compra.Aprobador = "Gerencia Operaciones";
                                    compra.IdEstadoCompra = (int)Valores.EstadoCompraEnm.PendienteAprobacion;
                                    break;
                                }
                            }
                            else
                            {
                                compra.Aprobador = "Gerencia Operaciones";
                                compra.IdEstadoCompra = (int)Valores.EstadoCompraEnm.PendienteAprobacion;
                                break;
                            }
                        }

                        if (compra.IdEstadoCompra == (int)Valores.EstadoCompraEnm.Abierta)
                        {
                            compra.Aprobador = "Sistema";
                            compra.IdEstadoCompra = (int)Valores.EstadoCompraEnm.Aprobada;
                        }
                    }
                }
            }
            else
            {
                if (compra.IdEstadoCompra == (int)Valores.EstadoCompraEnm.Aprobada && User.IsInRole("Compras"))
                {
                    compra.IdEstadoCompra = (int)Valores.EstadoCompraEnm.PendienteAprobacion;
                }
            }

            if (User.IsInRole("Compras"))
            {
                compra.MontoEnvio = model.MontoEnvio;
                compra.LugarEntrega = model.LugarEntrega;
            }

            compra.Subtotal = compra.Detalles.Sum(c => c.Subtotal);
            compra.Iva = compra.Detalles.Sum(c => c.SubtotalIva);

            compraService.Edit(compra);

            var final = CargarCompras(model.Id).First();

            return Json(new { success = true, data = final, message = Valores.Edicion });
        }

        [HttpGet]
        public IActionResult EditEstado(int id)
        {
            var result = compraService.GetById(id);

            ViewBag.IdEstadoCompra = new SelectList(estadoCompraService.GetAll(), "Id", "Nombre", result.IdEstadoCompra);

            return PartialView("_ModalEstado", result);
        }

        [HttpPost]
        public IActionResult EditEstado(Compra model)
        {
            var compra = compraService.GetById(model.Id);

            compra.IdEstadoCompra = model.IdEstadoCompra;

            compraService.Edit(compra);

            var final = CargarCompras(model.Id).First();

            return Json(new { success = true, data = final, message = Valores.Edicion });
        }

        [HttpGet]
        public IActionResult VerCompra(int id)
        {
            var result = compraService.GetSingle(c => c.Id == id, c => c.Solicitante.Rol, c => c.EstadoCompra, c => c.CentroCostos, c => c.Proveedor);

            var detalle = detalleCompraService.GetList(c => c.IdCompra == result.Id, c => c.Articulo);

            var negocio = negocioService.GetAll().First();

            var aprobar = (User.IsInRole("Administrador") || User.IsInRole("Gerencia General") || User.IsInRole("Gerencia Operaciones"))
                    && result.IdEstadoCompra == (int)Valores.EstadoCompraEnm.PendienteAprobacion;

            var final = new VerCompraVM
            {
                Solicitado = result.Fecha.ToString("dd/MM/yyyy HH:mm"),
                Plazo = result.FechaEntrega.ToString("dd/MM/yyyy"),
                CentroCostos = result.CentroCostos.Nombre,
                Area = result.Solicitante.Rol.Nombre,
                Proveedor = result.Proveedor?.Alias,
                Presupuesto1 = result.Presupuesto1,
                Presupuesto2 = result.Presupuesto2,
                Presupuesto3 = result.Presupuesto3,
                TipoCompra = ((Valores.TipoCompraEnum)result.IdTipoCompra).ToString(),
                Usuario = result.Solicitante.Nombre,
                CondicionPago = ((Valores.CondicionPagoOrdenEnum)result.IdCondicionPago).ToString(),
                Estado = $"<span class='text-{result.EstadoCompra.Color}'>{result.EstadoCompra.Nombre}</span>",
                FormaPago = result.IdFormaPago + " dias",
                Aprobador = result.Aprobador,
                Total = detalle.Sum(c => c.Total).ToString("C0"),
                Subtotal = detalle.Sum(c => c.Subtotal).ToString("C0"),
                IVA = detalle.Sum(c => c.SubtotalIva).ToString("C0"),
                Aprobar = aprobar,
                Id = result.Id,
                VerObservaciones = result.IdCondicionPago == (int)Valores.CondicionPagoOrdenEnum.Otro,
                Obsrvaciones = result.ObservacionPago,
                Provincia = result.IdProvincia,
                Articulos = detalle.Select(x => new VerCompraArticulosVM
                {
                    Nombre = x.Articulo.Nombre,
                    Cantidad = x.Cantidad + "",
                    Precio = x.Precio.ToString("C0"),
                    Unidad = x.Articulo.UnidMedida,
                    Subtotal = x.Subtotal.ToString("C0"),
                    Id = x.IdArticulo,
                    PrecioNumber = x.Precio
                }).ToList()
            };

            foreach (var item in final.Articulos)
            {
                var ultima = detalleCompraService.GetList(c => c.IdArticulo == item.Id && c.Compra.IdProvincia == final.Provincia && c.IdCompra != id && c.Compra.IdEstadoCompra == (int)Valores.EstadoCompraEnm.Aprobada && !c.Compra.Ignorar, c => c.Compra.Proveedor).LastOrDefault();

                if (ultima != null)
                {
                    var calculo = ultima.Precio * (1 + (negocio.MontoDiferencia / 100.0));

                    if (item.PrecioNumber > calculo)
                    {
                        item.Detalle = $"<div class='text-left'><b>Ultima compra:</b> {ultima.Compra.Fecha.ToString("dd/MM/yyyy")}<br><b>Orden de Compra:</b> {ultima.Compra.Codigo}<br><b>Proveedor:</b> {ultima.Compra.Proveedor?.Alias}<br><b>Precio uni:</b> ${ultima.Precio}</div>";
                    }
                }

                else
                {
                    item.Detalle = $"<div class='text-left'><b>Primera compra de este artículo</b> </div>";

                }
            }

            return PartialView("_ModalVerCompra", final);
        }

        public IActionResult Aprobar(int id)
        {
            var model = compraService.GetById(id);

            if (User.IsInRole("Gerencia Operaciones") && model.IdTipoCompra == (int)Valores.TipoCompraEnum.General)
            {
                return Json(new { success = false, message = "Tu rol no permite aprobar compras General" });
            }

            model.IdEstadoCompra = (int)Valores.EstadoCompraEnm.Aprobada;
            model.FechaUltimoEstado = CurrentDate;

            compraService.Edit(model);

            var final = CargarCompras(model.Id).First();

            return Json(new { success = true, data = final, message = Enum.Valores.Edicion });
        }

        public IActionResult Rechazar(int id)
        {
            var model = compraService.GetById(id);

            model.IdEstadoCompra = (int)Valores.EstadoCompraEnm.Rechazada;
            model.FechaUltimoEstado = CurrentDate;

            compraService.Edit(model);

            var final = CargarCompras(model.Id).First();

            return Json(new { success = true, data = final, message = Enum.Valores.Edicion });
        }

        public IActionResult Cancelar(int id)
        {
            var model = compraService.GetById(id);

            model.IdEstadoCompra = (int)Valores.EstadoCompraEnm.Cancelada;

            compraService.Edit(model);

            var final = CargarCompras(model.Id).First();

            return Json(new { success = true, data = final, message = Enum.Valores.Edicion });
        }

        public IActionResult Formulario(int id)
        {
            var model = compraService.GetSingle(c => c.Id == id, c => c.Proveedor, c => c.Solicitante);

            model.CondicionPago = ((Valores.CondicionPagoOrdenEnum)model.IdCondicionPago).ToString();
            model.FormaPago = model.IdFormaPago + " dias";

            model.Detalles = detalleCompraService.GetList(c => c.IdCompra == id, c => c.Articulo).ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult Partir(string id, int idCompra)
        {
            var ids = id.Split(',').Select(c => int.Parse(c));

            var compraExistente = compraService.GetById(idCompra);

            compraExistente.IdProveedor = null;
            compraExistente.Id = 0;

            if (compraExistente.IdProvincia == (int)Valores.ProvinciaEnum.Cordoba)
            {
                var fix = 001260;
                compraExistente.Codigo = $"01-{fix + compraService.GetList(c => c.IdProvincia == (int)Valores.ProvinciaEnum.Cordoba).Count()}";
            }
            else
            {
                compraExistente.Codigo = $"02-{"000000" + compraService.GetList(c => c.IdProvincia == (int)Valores.ProvinciaEnum.BuenosAires).Count()}";
            }

            compraService.Add(compraExistente);

            foreach (var item in ids)
            {
                var detalle = detalleCompraService.GetById(item);

                detalle.IdCompra = compraExistente.Id;

                detalleCompraService.Edit(detalle);
            }

            ActualizarTotal(idCompra);
            ActualizarTotal(compraExistente.Id);

            return Json(new { success = true, message = $"Se ha creado la nueva OC {compraExistente.Codigo} con los artículos seleccionados" });
        }

        private void ActualizarTotal(int id)
        {
            var compra = compraService.GetById(id);

            var detalles = detalleCompraService.GetList(c => c.IdCompra == id);

            compra.Subtotal = detalles.Sum(c => c.Subtotal);
            compra.Iva = detalles.Sum(c => c.SubtotalIva);

            compraService.Edit(compra);
        }

        [HttpGet]
        public IActionResult Exportar(int id)
        {
            var items = detalleCompraService.GetList(c => c.IdCompra == id, c => c.Articulo).Select(c => new ItemSeleccionableVM
            {
                Id = c.Id,
                Nombre = c.Articulo.Nombre,
                Seleccionado = true,
            });

            return PartialView("_ModalExportar", new VerExportarVM
            {
                Id = id,
                Items = items
            });
        }

        [HttpPost]
        public IActionResult Exportar(VerExportarVM model)
        {
            //var compra = compraService.GetById(model.Id);

            TempData["Instrucciones"] = model.Instrucciones;
            TempData["LugarEntrega"] = model.LugarEntrega;
            TempData["Items"] = model.Seleccionados;

            return Json(new { success = true });
        }

        public IActionResult Presupuesto(int id)
        {
            var model = compraService.GetSingle(c => c.Id == id, c => c.Proveedor, c => c.Solicitante);

            //model.CondicionPago = ((Valores.CondicionPagoOrdenEnum)model.IdCondicionPago).ToString();
            //model.FormaPago = model.IdFormaPago + " dias";

            var items = ((string)TempData["Items"]).Split(',').Select(c => int.Parse(c)).ToArray();

            model.Detalles = detalleCompraService.GetList(c => items.Contains(c.Id), c => c.Articulo).ToList();

            ViewData["Instrucciones"] = TempData["Instrucciones"];
            ViewData["LugarEntrega"] = TempData["LugarEntrega"];

            return View(model);
        }

        public IActionResult Ignorar(int id)
        {
            var model = compraService.GetById(id);

            model.Ignorar = !model.Ignorar;

            compraService.Edit(model);

            var final = CargarCompras(model.Id).First();

            return Json(new { success = true, data = final, message = Enum.Valores.Edicion });
        }

        [HttpGet]
        public IActionResult Historial(int id)
        {
            var historiales = historialCompraService.GetList(c => c.IdCompra == id);

            return PartialView("_ModalHistorial", historiales);
        }
        #endregion

        #region PROVEEDORES
        [HttpGet]
        public IActionResult Proveedores()
        {
            ViewData["Title"] = "Proveedores";

            return View();
        }

        [HttpGet]
        public IActionResult CargarTablaProveedores()
        {
            var final = CargarProveedores(null);

            return Json(new { success = true, data = final });
        }

        private IEnumerable<object> CargarProveedores(int? id)
        {
            return proveedorService.GetList(c => (id == null ? true : c.Id == id))
                .Select(c => new
                {
                    id = c.Id,
                    nombre = c.Alias,
                    email = c.Email,
                    telefono = c.Telefono,
                    activo = c.Activo,
                    etiqueta = c.Etiquetas
                })
                .OrderBy(c => c.nombre);
        }

        [HttpGet]
        public IActionResult CreateProveedor(int id)
        {
            ViewBag.IdCondicionPago = new SelectList(Valores.CondicionPagoEnum);

            return PartialView("_ModalProveedor", new Proveedor
            {

            });
        }

        [HttpPost]
        public IActionResult CreateProveedor(Proveedor model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = Valores.Incorrectos });
            }

            proveedorService.Add(model);

            var final = CargarProveedores(model.Id).First();

            return Json(new { success = true, data = final, message = Valores.Creacion });
        }

        [HttpGet]
        public IActionResult EditProveedor(int id)
        {
            var result = proveedorService.GetById(id);

            ViewBag.IdCondicionPago = new SelectList(Valores.CondicionPagoEnum, result.IdCondicionPago);

            return PartialView("_ModalProveedor", result);
        }

        [HttpPost]
        public IActionResult EditProveedor(Proveedor model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = Valores.Incorrectos });
            }

            var proveedor = proveedorService.GetById(model.Id);

            proveedor.RazonSocial = model.RazonSocial;
            proveedor.Alias = model.Alias;
            proveedor.Telefono = model.Telefono;
            proveedor.Email = model.Email;
            proveedor.CUIT = model.CUIT;
            proveedor.Domicilio = model.Domicilio;
            proveedor.Localidad = model.Localidad;
            proveedor.Contacto = model.Contacto;
            proveedor.Notas = model.Notas;
            proveedor.Banco = model.Banco;
            proveedor.CBU = model.CBU;
            proveedor.RetencionGanancias = model.RetencionGanancias;
            proveedor.DireccionFiscal = model.DireccionFiscal;
            proveedor.LocalidadFiscal = model.LocalidadFiscal;
            proveedor.CPFiscal = model.CPFiscal;
            proveedor.IdCondicionIva = model.IdCondicionIva;
            proveedor.IdCondicionPago = model.IdCondicionPago;
            proveedor.IdProvincia = model.IdProvincia;
            proveedor.IdProvinciaFiscal = model.IdProvinciaFiscal;
            proveedor.IIBB = model.IIBB;
            proveedor.Etiquetas = model.Etiquetas;

            proveedorService.Edit(proveedor);

            var final = CargarProveedores(model.Id).First();

            return Json(new { success = true, data = final, message = Valores.Edicion });
        }

        public IActionResult ActivarProveedor(int id)
        {
            var model = proveedorService.GetById(id);

            model.Activo = !model.Activo;

            proveedorService.Edit(model);

            var final = CargarProveedores(model.Id).First();

            return Json(new { success = true, data = final, message = Enum.Valores.Edicion });
        }

        [HttpGet]
        public IActionResult ImportarProveedor()
        {
            var model = new ExcelVM
            {
                Url = "/files/ExcelModeloProveedores.xlsx"
            };

            return PartialView("_ImportarProveedor", model);
        }

        [HttpPost]
        public async Task<IActionResult> ImportarProveedor(ExcelVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { success = false, message = Valores.Incorrectos });
                }

                if (model.File == null || model.File.Length == 0)
                {
                    return Json(new { success = false, message = "Debes seleccionar un archivo primero" });
                }

                var uploads = Path.Combine(hostingEnvironment.WebRootPath, "files");

                var nombre = Path.GetRandomFileName() + ".xlsx";

                var filePath = Path.Combine(uploads, nombre);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.File.CopyToAsync(stream);
                }

                var file = new FileInfo(filePath);

                var exitos = new List<string>();
                var errores = new List<string>();

                using (ExcelPackage package = new ExcelPackage(file))
                {
                    var sb = new StringBuilder();
                    var worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;
                    var colCount = worksheet.Dimension.Columns;

                    if (rowCount == 1)
                    {
                        var version = worksheet.Cells[1, 1].Value.ToString();

                        if (version != "1")
                        {
                            return Json(new { success = false, message = $"La version de tu archivo Excel no es la última. Porfavor descargala." });
                        }
                    }

                    var validadRepetidos = new List<string>();

                    for (int row = 3; row <= rowCount; row++)
                    {
                        try
                        {
                            if (worksheet.Cells[row, 1]?.Value?.ToString() == null)
                            {
                                continue;
                            }

                            var nuevo = new Proveedor
                            {
                                Alias = worksheet.Cells[row, 1]?.Value.ToString(),
                                RazonSocial = worksheet.Cells[row, 2]?.Value.ToString(),
                                CUIT = worksheet.Cells[row, 3]?.Value.ToString(),
                                Email = worksheet.Cells[row, 4]?.Value.ToString(),
                                Telefono = worksheet.Cells[row, 5]?.Value.ToString(),
                                Domicilio = worksheet.Cells[row, 6]?.Value.ToString(),
                                Localidad = worksheet.Cells[row, 7]?.Value.ToString(),
                                Contacto = worksheet.Cells[row, 8]?.Value.ToString(),
                                IdCondicionIva = 0,
                                IdCondicionPago = 0,
                                IdProvincia = 0,
                                IdProvinciaFiscal = 0
                            };

                            try
                            {
                                proveedorService.Add(nuevo);
                            }
                            catch (Exception e)
                            {
                                errores.Add($"{e.Message} en la fila {row}");
                                continue;
                            }

                            exitos.Add(nuevo.Alias);
                        }
                        catch (Exception e)
                        {
                            errores.Add($"Algunos valores en la fila {row} son incorrectos");
                            continue;
                        }
                    }
                }

                return Json(new { success = true, message = Valores.Edicion, exitos = exitos.ToArray(), errores = errores.ToArray() });
            }
            catch (System.Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }
        #endregion

        #region ARTICULOS
        [HttpGet]
        public IActionResult Articulos()
        {
            ViewData["Title"] = "Articulos";

            return View();
        }

        [HttpGet]
        public IActionResult CargarTablaArticulos()
        {
            var final = CargarArticulos(null);

            return Json(new { success = true, data = final });
        }

        private IEnumerable<object> CargarArticulos(int? id)
        {
            return articuloService.GetList(c => (id == null ? true : c.Id == id))
                .Select(c => new
                {
                    id = c.Id,
                    nombre = c.Nombre,
                    activo = c.Activo,
                    codigo = c.Codigo,
                    unidad = c.UnidMedida,
                    etiqueta = c.Etiquetas
                })
                .OrderBy(c => c.nombre);
        }

        [HttpGet]
        public IActionResult CreateArticulo(int id)
        {
            //ViewBag.IdProveedor = new SelectList(proveedorService.GetAll(), "Id", "Alias");

            return PartialView("_ModalArticulo", new Articulo
            {

            });
        }

        [HttpPost]
        public IActionResult CreateArticulo(Articulo model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = Valores.Incorrectos });
            }

            model.Activo = true;

            articuloService.Add(model);

            var final = CargarArticulos(model.Id).First();

            return Json(new { success = true, data = final, message = Valores.Creacion });
        }

        [HttpGet]
        public IActionResult EditArticulo(int id)
        {
            var result = articuloService.GetById(id);

            //ViewBag.IdProveedor = new SelectList(proveedorService.GetAll(), "Id", "Alias", result.IdProveedor);

            return PartialView("_ModalArticulo", result);
        }

        [HttpPost]
        public IActionResult EditArticulo(Articulo model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = Valores.Incorrectos });
            }

            var articulo = articuloService.GetById(model.Id);

            articulo.Nombre = model.Nombre;
            articulo.IdTipoArticulo = model.IdTipoArticulo;
            articulo.Descripcion = model.Descripcion;
            articulo.Observaciones = model.Observaciones;
            articulo.UnidMedida = model.UnidMedida;
            articulo.Codigo = model.Codigo;
            articulo.Etiquetas = model.Etiquetas;

            articuloService.Edit(articulo);

            var final = CargarArticulos(model.Id).First();

            return Json(new { success = true, data = final, message = Valores.Edicion });
        }

        public IActionResult ActivarArticulo(int id)
        {
            var model = articuloService.GetById(id);

            model.Activo = !model.Activo;

            articuloService.Edit(model);

            var final = CargarArticulos(model.Id).First();

            return Json(new { success = true, data = final, message = Enum.Valores.Edicion });
        }

        [HttpGet]
        public IActionResult ImportarArticulo()
        {
            var model = new ExcelVM
            {
                Url = "/files/ExcelModeloArticulos.xlsx"
            };

            return PartialView("_ImportarArticulo", model);
        }

        [HttpPost]
        public async Task<IActionResult> ImportarArticulo(ExcelVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { success = false, message = Valores.Incorrectos });
                }

                if (model.File == null || model.File.Length == 0)
                {
                    return Json(new { success = false, message = "Debes seleccionar un archivo primero" });
                }

                var uploads = Path.Combine(hostingEnvironment.WebRootPath, "files");

                var nombre = Path.GetRandomFileName() + ".xlsx";

                var filePath = Path.Combine(uploads, nombre);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.File.CopyToAsync(stream);
                }

                var file = new FileInfo(filePath);

                var exitos = new List<string>();
                var errores = new List<string>();

                using (ExcelPackage package = new ExcelPackage(file))
                {
                    var sb = new StringBuilder();
                    var worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;
                    var colCount = worksheet.Dimension.Columns;

                    if (rowCount == 1)
                    {
                        var version = worksheet.Cells[1, 1].Value.ToString();

                        if (version != "2")
                        {
                            return Json(new { success = false, message = $"La version de tu archivo Excel no es la última. Porfavor descargala." });
                        }
                    }

                    var validadRepetidos = new List<string>();

                    for (int row = 3; row <= rowCount; row++)
                    {
                        try
                        {
                            if (worksheet.Cells[row, 1]?.Value?.ToString() == null)
                            {
                                continue;
                            }

                            var nuevo = new Articulo
                            {
                                Nombre = worksheet.Cells[row, 1]?.Value.ToString(),
                                Codigo = worksheet.Cells[row, 2]?.Value.ToString(),
                                UnidMedida = worksheet.Cells[row, 3]?.Value.ToString(),
                                Etiquetas = worksheet.Cells[row, 4]?.Value.ToString(),
                                Activo = true
                            };

                            try
                            {
                                articuloService.Add(nuevo);
                            }
                            catch (Exception e)
                            {
                                errores.Add($"{e.Message} en la fila {row}");
                                continue;
                            }

                            exitos.Add(nuevo.Nombre);
                        }
                        catch (Exception e)
                        {
                            errores.Add($"Algunos valores en la fila {row} son incorrectos");
                            continue;
                        }
                    }
                }

                return Json(new { success = true, message = Valores.Edicion, exitos = exitos.ToArray(), errores = errores.ToArray() });
            }
            catch (System.Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }

        [HttpGet]
        public IActionResult HistorialArticulo(int id)
        {
            var items = detalleCompraService.GetList(c => c.IdArticulo == id
                && (c.Compra.IdEstadoCompra == (int)Valores.EstadoCompraEnm.Aprobada || c.Compra.IdEstadoCompra == (int)Valores.EstadoLogisticaEnm.Recibida || c.Compra.IdEstadoCompra == (int)Valores.EstadoLogisticaEnm.RecibidaParcial)
                , c => c.Compra).OrderByDescending(c => c.Id).Take(20);

            return PartialView("_ModalHistorialArticulo", items);
        }

        #endregion

        #region CENTRO DE COSTOS
        [HttpGet]
        public IActionResult CentroCostos()
        {
            ViewData["Title"] = "Centro de Costos";

            return View();
        }

        [HttpGet]
        public IActionResult CargarTablaCentroCostos()
        {
            var final = CargarCentroCostos(null);

            return Json(new { success = true, data = final });
        }

        private IEnumerable<object> CargarCentroCostos(int? id)
        {
            return centroCostosService.GetList(c => (id == null ? true : c.Id == id))
                .Select(c => new
                {
                    id = c.Id,
                    codigo = c.Codigo,
                    nombre = c.Nombre
                })
                .OrderBy(c => c.nombre);
        }

        [HttpGet]
        public IActionResult CreateCentroCostos()
        {
            return PartialView("_ModalCentroCostos", new CentroCostos
            {

            });
        }

        [HttpPost]
        public IActionResult CreateCentroCostos(CentroCostos model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = Valores.Incorrectos });
            }

            centroCostosService.Add(model);

            var final = CargarCentroCostos(model.Id).First();

            return Json(new { success = true, data = final, message = Valores.Creacion });
        }

        [HttpGet]
        public IActionResult EditCentroCostos(int id)
        {
            var result = centroCostosService.GetById(id);

            return PartialView("_ModalCentroCostos", result);
        }

        [HttpPost]
        public IActionResult EditCentroCostos(CentroCostos model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = Valores.Incorrectos });
            }

            var result = centroCostosService.GetById(model.Id);

            result.Nombre = model.Nombre;
            result.Codigo = model.Codigo;

            centroCostosService.Edit(result);

            var final = CargarCentroCostos(model.Id).First();

            return Json(new { success = true, data = final, message = Valores.Edicion });
        }
        #endregion

        [HttpGet]
        public IActionResult Montos()
        {
            var result = negocioService.GetAll().First();

            return PartialView("_ModalMonto", result);
        }

        [HttpPost]
        public IActionResult Montos(Negocio model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = Valores.Incorrectos });
            }

            var result = negocioService.GetById(model.Id);

            result.MontoDiferencia = model.MontoDiferencia;
            result.MontoMinimo = model.MontoMinimo;

            negocioService.Edit(result);

            return Json(new { success = true, message = Valores.Edicion });
        }

        [HttpGet]
        public IActionResult Notificaciones()
        {
            var usuario = usuarioService.GetByEmail(User.Identity.Name);

            var final = new List<NotificacionVM>();

            if (User.IsInRole("Compras"))
            {
                final = compraService.GetList(c => c.IdProvincia == usuario.IdProvincia &&
                (c.IdEstadoCompra == (int)Valores.EstadoCompraEnm.Aprobada || c.IdEstadoCompra == (int)Valores.EstadoCompraEnm.Rechazada))
                .OrderByDescending(c => c.FechaUltimoEstado)
                .Take(20)
                .Select(c => new NotificacionVM
                {
                    Nombre = $"OC {c.Codigo}",
                    Fecha = c.FechaUltimoEstado?.ToString("dd/MM HH:mm"),
                    Icono = c.IdEstadoCompra == (int)Valores.EstadoCompraEnm.Aprobada ? "fa fa-check text-success" : (c.IdEstadoCompra == (int)Valores.EstadoCompraEnm.Rechazada ? "fa fa-times text-danger" : "fa fa-clock text-warning"),
                    Leida = c.FechaUltimoEstado < usuario.UltimaNotificacion,
                    IdCompra = c.Id
                })
                .ToList();
            }
            else
            {
                final = compraService.GetList(c => c.IdUsuario == usuario.Id &&
                (c.IdEstadoCompra == (int)Valores.EstadoCompraEnm.Aprobada || c.IdEstadoCompra == (int)Valores.EstadoCompraEnm.Rechazada))
                .OrderByDescending(c => c.FechaUltimoEstado)
                .Take(20)
                .Select(c => new NotificacionVM
                {
                    Nombre = $"OC {c.Codigo}",
                    Fecha = c.FechaUltimoEstado?.ToString("dd/MM HH:mm"),
                    Icono = c.IdEstadoCompra == (int)Valores.EstadoCompraEnm.Aprobada ? "fa fa-check text-success" : (c.IdEstadoCompra == (int)Valores.EstadoCompraEnm.Rechazada ? "fa fa-times text-danger" : "fa fa-clock text-warning"),
                    Leida = c.FechaUltimoEstado < usuario.UltimaNotificacion,
                    IdCompra = c.Id
                })
                .ToList();
            }

            usuario.UltimaNotificacion = CurrentDate;

            usuarioService.Edit(usuario);

            return PartialView("_Notificaciones", final);
        }
    }
}