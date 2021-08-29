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
        private readonly IProveedorService proveedorService;
        private readonly IArticuloService articuloService;
        private readonly IRolService rolService;
        private readonly INegocioService negocioService;
        private readonly IWebHostEnvironment hostingEnvironment;

        public AdminController(
            IUsuarioService usuarioService,
            IProveedorService proveedorService,
            IArticuloService articuloService,
            IRolService rolService,
            INegocioService negocioService,

            IWebHostEnvironment hostingEnvironment)
        {
            this.usuarioService = usuarioService;
            this.proveedorService = proveedorService;
            this.articuloService = articuloService;
            this.rolService = rolService;
            this.negocioService = negocioService;
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
            return usuarioService.GetList(c => (id == null ? true : c.Id == id), c => c.Rol)
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

            model.Activo = false;
            model.Email = "eliminado_" + model.Email;

            usuarioService.Edit(model);

            return Json(new { success = true, message = Enum.Valores.Eliminacion });
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

            proveedor.Alias = model.Alias;
            proveedor.Telefono = model.Telefono;
            proveedor.Email = model.Email;
            proveedor.Domicilio = model.Domicilio;
            proveedor.Localidad = model.Localidad;
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
                            return Json(new { success = false, message = $"La version de tu archivo Excel no es la �ltima. Porfavor descargala." });
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
                                Email = worksheet.Cells[row, 2]?.Value.ToString(),
                                Telefono = worksheet.Cells[row, 3]?.Value.ToString(),
                                Domicilio = worksheet.Cells[row, 4]?.Value.ToString(),
                                Localidad = worksheet.Cells[row, 5]?.Value.ToString(),
                                NombreContacto = worksheet.Cells[row, 6]?.Value.ToString()
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
                            return Json(new { success = false, message = $"La version de tu archivo Excel no es la �ltima. Porfavor descargala." });
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
        #endregion

        [HttpGet]
        public IActionResult Negocio()
        {
            var result = negocioService.GetAll().First();

            return PartialView("_ModalNegocio", result);
        }

        [HttpPost]
        public IActionResult Negocio(Negocio model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = Valores.Incorrectos });
            }

            var result = negocioService.GetById(model.Id);

            negocioService.Edit(result);

            return Json(new { success = true, message = Valores.Edicion });
        }
    }
}