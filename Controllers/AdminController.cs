using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using netCoreNew.Business;
using netCoreNew.Enum;
using netCoreNew.Helpers;
using netCoreNew.Models;
using netCoreNew.ViewModels;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private readonly IProyectoService proyectoService;
        private readonly IRecuentoService recuentoService;
        private readonly IDetalleRecuentoService detalleRecuentoService;
        private readonly ICodigoProveedorService codigoProveedorService;
        private readonly IWebHostEnvironment hostingEnvironment;


        public AdminController(
            IUsuarioService usuarioService,
            IProveedorService proveedorService,
            IArticuloService articuloService,
            IRolService rolService,
            INegocioService negocioService,
            IProyectoService proyectoService,
            IRecuentoService recuentoService,
            IDetalleRecuentoService detalleRecuentoService,
            ICodigoProveedorService codigoProveedorService,

            IWebHostEnvironment hostingEnvironment)
        {
            this.usuarioService = usuarioService;
            this.proveedorService = proveedorService;
            this.articuloService = articuloService;
            this.rolService = rolService;
            this.negocioService = negocioService;
            this.proyectoService = proyectoService;
            this.recuentoService = recuentoService;
            this.detalleRecuentoService = detalleRecuentoService;
            this.codigoProveedorService = codigoProveedorService;
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
            return proveedorService.GetList(c => (id == null ? !c.Eliminado : c.Id == id))
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

        public IActionResult EliminarProveedor(int id)
        {
            var model = proveedorService.GetById(id);

            model.Eliminado = true;

            proveedorService.Edit(model);

            var final = CargarProveedores(model.Id).First();

            return Json(new { success = true, data = final, message = Enum.Valores.Edicion });
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
            var codigos = codigoProveedorService.GetAll().ToArray();

            return articuloService.GetList(c => (id == null ? !c.Eliminado : c.Id == id))
                .Select(c => new
                {
                    id = c.Id,
                    nombre = c.Nombre,
                    activo = c.Activo,
                    codigo = c.Codigo,
                    codigoRichetta = (codigos.Where(x => x.IdArticulo == c.Id && x.IdProveedor == (int)ProveedoresEnum.Richetta).FirstOrDefault()?.Codigo)?? "-", 
                    codigoSchneider = codigos.Where(x => x.IdArticulo == c.Id && x.IdProveedor == (int)ProveedoresEnum.Schneider).FirstOrDefault()?.Codigo?? "-", 
                    marca = c.Marca,
                    unidad = c.UnidMedida,
                    etiqueta = c.Etiquetas,
                    precio = c.Precio.ToString("C1"),
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
            articulo.Marca = model.Marca;

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

        public IActionResult EliminarArticulo(int id)
        {
            var model = articuloService.GetById(id);

            model.Eliminado = true;

            articuloService.Edit(model);

            var final = CargarArticulos(model.Id).First();

            return Json(new { success = true, data = final, message = Enum.Valores.Edicion });
        }

        [HttpGet]
        public IActionResult ImportarArticulo()
        {
            var model = new ExcelVM
            {
                Url = ExcelArticulos()
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

                    //if (rowCount == 1)
                    //{
                    //    var version = worksheet.Cells[1, 1].Value.ToString();

                    //    if (version != "2")
                    //    {
                    //        return Json(new { success = false, message = $"La version de tu archivo Excel no es la última. Porfavor descargala." });
                    //    }
                    //}

                    var validadRepetidos = new List<string>();

                    for (int row = 2; row <= rowCount; row++)
                    {
                        try
                        {
                            if (worksheet.Cells[row, 1]?.Value?.ToString() != null)
                            {
                                var id = int.Parse(worksheet.Cells[row, 1].Value.ToString());

                                var articulo = articuloService.GetById(id);

                                articulo.Id = Convert.ToInt32(worksheet.Cells[row, 1]?.Value);
                                articulo.Nombre = worksheet.Cells[row, 2]?.Value.ToString();
                                articulo.Codigo = worksheet.Cells[row, 3]?.Value.ToString();
                                articulo.Descripcion = worksheet.Cells[row, 4]?.Value?.ToString();
                                articulo.Marca = worksheet.Cells[row, 5]?.Value.ToString();
                                articulo.Precio = Convert.ToDouble(worksheet.Cells[row, 6]?.Value);
                                articulo.Observaciones = worksheet.Cells[row, 7]?.Value?.ToString();
                                articulo.UnidMedida = worksheet.Cells[row, 8]?.Value.ToString();
                                articulo.Activo = worksheet.Cells[row, 9]?.Value.ToString() == "si" ? true : false;
                                articulo.Etiquetas = worksheet.Cells[row, 10]?.Value.ToString();
                                articulo.Eliminado = false;
                                
                                //aca falta un firstOrdefault?
                                //var idDetalleRichetta = codigoProveedorService.GetList(c => c.IdArticulo == articulo.Id && c.IdProveedor == (int)ProveedoresEnum.Richetta);
                                //var idDetalleSchneijder = codigoProveedorService.GetList(c => c.IdArticulo == articulo.Id && c.IdProveedor == (int)ProveedoresEnum.Schneider);

                                if (worksheet.Cells[row, 11]?.Value != null)
                                {
                                    articulo.Detalles.Add(new CodigoProveedor
                                    {
                                        IdArticulo = Convert.ToInt32(worksheet.Cells[row, 1]?.Value),
                                        IdProveedor = (int)ProveedoresEnum.Richetta,
                                        Codigo = worksheet.Cells[row, 11]?.Value?.ToString(),
                                        PrecioProveedor = Convert.ToDouble(worksheet.Cells[row, 12]?.Value),
                                    });
                                }

                                if (worksheet.Cells[row, 14]?.Value != null)
                                {
                                    articulo.Detalles.Add(new CodigoProveedor
                                    {
                                        IdArticulo = Convert.ToInt32(worksheet.Cells[row, 1]?.Value),
                                        IdProveedor = (int)ProveedoresEnum.Schneider,
                                        Codigo = worksheet.Cells[row, 13]?.Value.ToString(),
                                        PrecioProveedor = (Double)worksheet.Cells[row, 14]?.Value,
                                    });
                                }

                                articuloService.Edit(articulo);

                                foreach (var detalle in articulo.Detalles)
                                {
                                    codigoProveedorService.Edit(detalle);
                                };

                                continue;
                            }

                            if (worksheet.Cells[row, 2]?.Value?.ToString() == null)
                            {
                                //nada mas
                                break;
                            }

                            var nuevo = new Articulo
                            {
                                
                                Nombre = worksheet.Cells[row, 2]?.Value.ToString(),
                                Codigo = worksheet.Cells[row, 3]?.Value.ToString(),
                                Descripcion = worksheet.Cells[row, 4]?.Value?.ToString(),
                                Marca = worksheet.Cells[row, 5]?.Value?.ToString(),
                                Precio = worksheet.Cells[row, 6]?.Value == null? 0 : Convert.ToDouble(worksheet.Cells[row, 6]?.Value),
                                Observaciones = worksheet.Cells[row, 7]?.Value?.ToString(),
                                UnidMedida = worksheet.Cells[row, 8]?.Value?.ToString(),
                                Activo = worksheet.Cells[row, 9]?.Value?.ToString() == "si" ? true : false,
                                Etiquetas = worksheet.Cells[row, 10]?.Value?.ToString(),
                                Eliminado = false,
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

                            nuevo.Id = articuloService.GetSingle(c => c.Codigo == nuevo.Codigo).Id;

                            nuevo.Detalles.Add(new CodigoProveedor
                            {
                                IdArticulo = nuevo.Id,
                                IdProveedor = (int)ProveedoresEnum.Richetta,
                                Codigo = worksheet.Cells[row, 11]?.Value?.ToString(),
                                PrecioProveedor = worksheet.Cells[row, 12]?.Value == null? 0 :Convert.ToDouble(worksheet.Cells[row, 12]?.Value),
                            });

                            nuevo.Detalles.Add(new CodigoProveedor
                            {
                                IdArticulo = nuevo.Id,
                                IdProveedor = (int)ProveedoresEnum.Schneider,
                                Codigo = worksheet.Cells[row, 13]?.Value.ToString(),
                                PrecioProveedor = (Double)worksheet.Cells[row, 14]?.Value,
                            });


                            try
                            {
                                //articuloService.Add(nuevo);

                                foreach (var detalle in nuevo.Detalles)
                                {
                                    detalle.Id = 0;
                                    codigoProveedorService.Add(detalle);
                                };

                            }
                            catch (Exception e)
                            {
                                errores.Add($"{e.Message} en la fila {row}");
                                continue;
                            }

                            //exitos.Add(nuevo.Nombre);
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

        public string ExcelArticulos()
        {
            var folder = "files";

            var uploads = Path.Combine(hostingEnvironment.WebRootPath, folder);

            var nombre = Path.GetRandomFileName() + ".xlsx";

            var filePath = Path.Combine(uploads, nombre);

            //using (var stream = new FileStream(filePath, FileMode.Create))
            //{
            //    await model.File.CopyToAsync(stream);
            //}

            var file = new FileInfo(filePath);

            using (ExcelPackage package = new ExcelPackage(file))
            {
                var worksheet = package.Workbook.Worksheets.Add("MIS ARTICULOS");

                var column = 1;

                worksheet.Cells[1, column++].Value = "Id (NO TOCAR)";
                worksheet.Cells[1, column++].Value = "Artículo";
                worksheet.Cells[1, column++].Value = "Codigo General";
                worksheet.Cells[1, column++].Value = "Descripción";
                worksheet.Cells[1, column++].Value = "Marca";
                worksheet.Cells[1, column++].Value = "Precio";
                worksheet.Cells[1, column++].Value = "Observaciones";
                worksheet.Cells[1, column++].Value = "Un. Medida";
                worksheet.Cells[1, column++].Value = "Activo (si o no)";
                worksheet.Cells[1, column++].Value = "Etiquetas (separadas por comas)";
                worksheet.Cells[1, column++].Value = "Codigo Richetta";
                worksheet.Cells[1, column++].Value = "Precio Richetta ";
                worksheet.Cells[1, column++].Value = "Código Schneider";
                worksheet.Cells[1, column++].Value = "PrecioSchenider";

                var _codigoproveedor = codigoProveedorService.GetAll()
                    .ToList();

                var _articulos = articuloService.GetList(c => !c.Eliminado)
                .AsEnumerable()
                .Select(c => new
                {
                    c.Id,
                    c.Nombre,
                    c.Codigo,
                    c.Descripcion,
                    c.Marca,
                    c.Precio,
                    c.Observaciones,
                    c.UnidMedida,
                    c.Activo,
                    c.Etiquetas,
                    codigoRichetta = _codigoproveedor.FirstOrDefault(x => x.IdArticulo == c.Id && x.IdProveedor == (int)ProveedoresEnum.Richetta)?.Codigo,
                    precioRichetta = _codigoproveedor.FirstOrDefault(x => x.IdArticulo == c.Id && x.IdProveedor == (int)ProveedoresEnum.Richetta)?.PrecioProveedor,
                    codigoSchneider = _codigoproveedor.FirstOrDefault(x => x.IdArticulo == c.Id && x.IdProveedor == (int)ProveedoresEnum.Schneider)?.Codigo,
                    precioSchneider = _codigoproveedor.FirstOrDefault(x => x.IdArticulo == c.Id && x.IdProveedor == (int)ProveedoresEnum.Schneider)?.PrecioProveedor
                })
                .OrderBy(c => c.Nombre);

                var _rowPublicaciones = 2;
                var totalArticulos = 0;

                foreach (var item in _articulos)
                {
                    column = 1;

                    worksheet.Cells[_rowPublicaciones, column++].Value = item.Id;
                    worksheet.Cells[_rowPublicaciones, column++].Value = item.Nombre;
                    worksheet.Cells[_rowPublicaciones, column++].Value = item.Codigo;
                    worksheet.Cells[_rowPublicaciones, column++].Value = item.Descripcion;
                    worksheet.Cells[_rowPublicaciones, column++].Value = item.Marca;
                    worksheet.Cells[_rowPublicaciones, column++].Value = item.Precio;
                    worksheet.Cells[_rowPublicaciones, column++].Value = item.Observaciones;
                    worksheet.Cells[_rowPublicaciones, column++].Value = item.UnidMedida;
                    worksheet.Cells[_rowPublicaciones, column++].Value = item.Activo ? "si" : "no";
                    worksheet.Cells[_rowPublicaciones, column++].Value = item.Etiquetas;
                    worksheet.Cells[_rowPublicaciones, column++].Value = item.codigoRichetta;
                    worksheet.Cells[_rowPublicaciones, column++].Value = item.precioRichetta;
                    worksheet.Cells[_rowPublicaciones, column++].Value = item.codigoSchneider;
                    worksheet.Cells[_rowPublicaciones, column++].Value = item.precioSchneider;

                    _rowPublicaciones++;
                }

                totalArticulos = _rowPublicaciones;

                //var _categorias = categoriaService.GetList(c => c.IdNegocio == NegocioHelper.GetCurrentNegocioId)
                //.Select(c => new
                //{
                //    c.Id,
                //    c.Nombre
                //})
                // .OrderBy(c => c.Nombre);
                //_rowPublicaciones = 2;
                //foreach (var item in _categorias)
                //{
                //    worksheet.Cells[_rowPublicaciones, (int)Valores.ColumnaExcel.IdCategoria].Value = item.Id;
                //    worksheet.Cells[_rowPublicaciones, (int)Valores.ColumnaExcel.CategoriaNombre].Value = item.Nombre;
                //    _rowPublicaciones++;
                //}

                //var _subcategorias = subCategoriaService.GetList(c => c.Categoria.IdNegocio == NegocioHelper.GetCurrentNegocioId, c => c.Categoria)
                //.Select(c => new
                //{
                //    c.Id,
                //    Nombre = c.Categoria.Nombre + " | " + c.Nombre
                //})
                //.OrderBy(c => c.Nombre);
                //_rowPublicaciones = 2;
                //foreach (var item in _subcategorias)
                //{
                //    worksheet.Cells[_rowPublicaciones, (int)Valores.ColumnaExcel.IdSubcategoria].Value = item.Id;
                //    worksheet.Cells[_rowPublicaciones, (int)Valores.ColumnaExcel.SubcategoriaNombre].Value = item.Nombre;
                //    _rowPublicaciones++;
                //}

                //var _marcas = marcaService.GetList(c => c.IdNegocio == NegocioHelper.GetCurrentNegocioId)
                //.Select(c => new
                //{
                //    c.Id,
                //    c.Nombre
                //})
                //.OrderBy(c => c.Nombre);
                //_rowPublicaciones = 2;
                //foreach (var item in _marcas)
                //{
                //    worksheet.Cells[_rowPublicaciones, (int)Valores.ColumnaExcel.IdMarca].Value = item.Id;
                //    worksheet.Cells[_rowPublicaciones, (int)Valores.ColumnaExcel.MarcaNombre].Value = item.Nombre;
                //    _rowPublicaciones++;
                //}

                using (var range = worksheet.Cells[1, 1, 1, column])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red);
                    range.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    range.Style.Font.Size = 11;
                }

                //using (var range = worksheet.Cells[1, (int)Valores.ColumnaExcel.IdCategoria, 1, (int)Valores.ColumnaExcel.CategoriaNombre])
                //{
                //    range.Style.Font.Bold = true;
                //    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                //    range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red);
                //    range.Style.Font.Color.SetColor(System.Drawing.Color.White);
                //    range.Style.Font.Size = 11;
                //}

                //using (var range = worksheet.Cells[1, (int)Valores.ColumnaExcel.IdSubcategoria, 1, (int)Valores.ColumnaExcel.SubcategoriaNombre])
                //{
                //    range.Style.Font.Bold = true;
                //    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                //    range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red);
                //    range.Style.Font.Color.SetColor(System.Drawing.Color.White);
                //    range.Style.Font.Size = 11;
                //}

                //using (var range = worksheet.Cells[1, (int)Valores.ColumnaExcel.IdMarca, 1, (int)Valores.ColumnaExcel.MarcaNombre])
                //{
                //    range.Style.Font.Bold = true;
                //    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                //    range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red);
                //    range.Style.Font.Color.SetColor(System.Drawing.Color.White);
                //    range.Style.Font.Size = 11;
                //}

                worksheet.Cells.AutoFitColumns(10, 100);  //Autofit columns for all cells

                //using (var range = worksheet.Cells[2, 1, totalArticulos, (int)Valores.ColumnaExcel.UltimoProducto])
                //{
                //    range.Style.Font.Size = 9;
                //    range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                //    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                //    range.Style.WrapText = true;
                //}

                //for (int i = 1; i <= (int)Valores.ColumnaExcel.MarcaNombre; i++)
                //{
                //    worksheet.Column(i).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                //    worksheet.Column(i).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                //    worksheet.Column(i).Style.WrapText = false;
                //}

                package.Save();
            }

            return Path.Combine("../" + folder, nombre);
        }

        #endregion

        #region PROYECTOS
        [HttpGet]
        public IActionResult Proyectos()
        {
            ViewData["Title"] = "Proyectos";

            return View();
        }

        [HttpGet]
        public IActionResult CargarTablaProyectos()
        {
            var final = CargarProyectos(null);

            return Json(new { success = true, data = final });
        }

        private IEnumerable<object> CargarProyectos(int? id)
        {
            return proyectoService.GetList(c => (id == null ? true : c.Id == id), c => c.Usuario)
                .Select(c => new
                {
                    id = c.Id,
                    nombre = c.Nombre,
                    usuario = c.Usuario.Nombre,
                    fecha = c.FechaCreacion.ToString("dd/MM/yyyy")
                })
                .OrderBy(c => c.nombre);
        }

        [HttpGet]
        public IActionResult CreateProyecto(int id)
        {
            ViewBag.IdRecuento = new SelectList(recuentoService.GetAll(), "Id", "Nombre");

            return PartialView("_ModalProyecto", new Proyecto
            {

            });
        }

        [HttpPost]
        public IActionResult CreateProyecto(Proyecto model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = Valores.Incorrectos });
            }

            model.FechaCreacion = CurrentDate;
            model.IdUsuario = usuarioService.GetByEmail(User.Identity.Name).Id;

            proyectoService.Add(model);

            var final = CargarProyectos(model.Id).First();

            return Json(new { success = true, data = final, message = Valores.Creacion });
        }

        [HttpGet]
        public IActionResult EditProyecto(int id)
        {
            var result = proyectoService.GetById(id);

            return PartialView("_ModalProyecto", result);
        }

        [HttpPost]
        public IActionResult EditProyecto(Proyecto model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = Valores.Incorrectos });
            }

            var proyecto = proyectoService.GetById(model.Id);

            proyecto.Nombre = model.Nombre;
            proyecto.FechaModificacion = CurrentDate;

            proyectoService.Edit(proyecto);

            var final = CargarProyectos(model.Id).First();

            return Json(new { success = true, data = final, message = Valores.Edicion });
        }

        public IActionResult CargarDatosRecuento(int id)
        {
            
            var detalles = detalleRecuentoService.GetList(c => c.IdRecuento == id, c=> c.Recuento);

            Double total = 0;

            foreach (var item in detalles)
            {
                total = total + item.Subtotal;
            }
            var final = new
            {
                fechaCreacion = detalles.FirstOrDefault().Recuento.FechaCreacion.ToString("dd/MM/yyyy"),
                total = total.ToString("C0")

            };            

            return Json(new
            {
                success = true,
                data = new
                {
                    final.fechaCreacion,
                    final.total
                    
                }
            });
        }
        #endregion

        #region RECUENTOS
        [HttpGet]
        public IActionResult Recuentos()
        {
            ViewData["Title"] = "Cómputos";

            return View();
        }

        [HttpGet]
        public IActionResult CargarTablaRecuentos()
        {
            var final = CargarRecuentos(null);

            return Json(new { success = true, data = final });
        }

        private IEnumerable<object> CargarRecuentos(int? id)
        {
            return recuentoService.GetList(c => (id == null ? !c.Eliminado : c.Id == id), c => c.Usuario, c => c.Detalles)
                .AsEnumerable()
                .Select(c => new
                {
                    id = c.Id,
                    nombre = c.Nombre,
                    usuario = c.Usuario.Nombre,
                    fecha = c.FechaCreacion.ToString("dd/MM/yyyy"),
                    total = c.Detalles.Sum(c => c.Subtotal).ToString("C1") + " " + (c.Detalles.Any(x => x.Precio == 0) ? "💸" : ""),
                    modificado = c.FechaModificacion?.ToString("dd/MM/yyyy"),
                    etiquetas = c.Etiquetas
                    
                })
                .OrderBy(c => c.nombre);
        }

        [HttpGet]
        public IActionResult CreateRecuento(int id)
        {
            var lista = articuloService.GetAll()
                .AsEnumerable()
                .Select(c => new
                {
                    c.Id,
                    Nombre = c.Nombre.TryTrim() + " - " + c.Marca + " - " + c.Etiquetas
                });

            ViewBag.IdArticulo = new SelectList(lista, "Id", "Nombre");          

            return PartialView("_ModalRecuento", new Recuento
            {

            });
        }

        [HttpPost]
        public IActionResult CreateRecuento(Recuento model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = Valores.Incorrectos });
            }

            model.FechaCreacion = CurrentDate;
            model.IdUsuario = usuarioService.GetByEmail(User.Identity.Name).Id;

            foreach (var item in model.Items)
            {
                model.Detalles.Add(new DetalleRecuento
                {
                    IdArticulo = item.IdArticulo,
                    Cantidad = item.Cantidad,
                    Precio = item.Precio,
                    UnidadMedida = item.UnidadMedida,
                    Codigo = item.Codigo                 
                });
            }

            recuentoService.Add(model);

            var final = CargarRecuentos(model.Id).First();

            return Json(new { success = true, data = final, message = Valores.Creacion });
        }

        [HttpGet]
        public IActionResult EditRecuento(int id)
        {
            var result = recuentoService.GetById(id);

            ViewBag.IdArticulo = new SelectList(articuloService.GetAll(), "Id", "NombreMarcaEtiquetas", "Codigo");

            var items = detalleRecuentoService.GetList(c => c.IdRecuento == id, c => c.Articulo).Select(c => new
            {
                Nombre = c.Codigo + "+" + c.IdArticulo + "+" + c.Cantidad + "+" + c.UnidadMedida + "+" + c.Precio + "+" + c.Id
            });

            result.ItemsLoad = string.Join(';', items.Select(c => c.Nombre));

            return PartialView("_ModalRecuento", result);
        }

        [HttpPost]
        public IActionResult EditRecuento(Recuento model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = Valores.Incorrectos });
            }

            var recuento = recuentoService.GetById(model.Id);

            var detalles = detalleRecuentoService.GetList(c => c.IdRecuento == model.Id);

            detalleRecuentoService.DeleteRange(detalles.ToArray());

            foreach (var item in model.Items)
            {
                recuento.Detalles.Add(new DetalleRecuento
                {
                    IdArticulo = item.IdArticulo,
                    Cantidad = item.Cantidad,
                    Precio = item.Precio,
                    UnidadMedida = item.UnidadMedida,
                    Codigo = item.Codigo
                });
            }

            recuento.Nombre = model.Nombre;
            recuento.FechaModificacion = CurrentDate;
            recuento.Etiquetas = model.Etiquetas;
            recuento.Descripcion = model.Descripcion;
           

            recuentoService.Edit(recuento);

            var final = CargarRecuentos(model.Id).First();

            return Json(new { success = true, data = final, message = Valores.Edicion });

        }

        [HttpPost]
        public IActionResult GuardarComo(Recuento model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = Valores.Incorrectos });
            }

            var recuento = recuentoService.GetById(model.Id);

            var detalles = detalleRecuentoService.GetList(c => c.IdRecuento == model.Id);

            foreach (var item in model.Items)
            {
                recuento.Detalles.Add(new DetalleRecuento
                {
                    IdArticulo = item.IdArticulo,
                    Cantidad = item.Cantidad,
                    Precio = item.Precio,
                    UnidadMedida = item.UnidadMedida,
                    Codigo = item.Codigo
                });
            }

            recuento.Nombre = model.Nombre + " 2";
            recuento.Etiquetas = model.Etiquetas;
            recuento.Descripcion = model.Descripcion;
            recuento.Id = 0;

            recuentoService.Add(recuento);

            var final = CargarRecuentos(recuento.Id).First();

            return Json(new { success = true, data = final, message = Valores.Creacion });
        }

        public IActionResult EliminarRecuento(int id)
        {
            var model = recuentoService.GetById(id);

            model.Eliminado = true;

            recuentoService.Edit(model);

            var final = CargarRecuentos(model.Id).First();

            return Json(new { success = true, data = final, message = Enum.Valores.Edicion });
        }

        [HttpGet]
        public IActionResult VerRecuento(int id)
        {
            var result = recuentoService.GetSingle(c => c.Id == id, c => c.Usuario);
            var detalle = detalleRecuentoService.GetList(c => c.IdRecuento == result.Id, c => c.Articulo);

            var final = new VerRecuentoVM
            {
                FechaCreacion = result.FechaCreacion.ToString("dd/MM/yyyy"),
                FechaModificacion = result.FechaModificacion?.ToString("dd/MM/yyyy"),
                Nombre = result.Nombre,
                CreadoPor = result.Usuario.Nombre,
                Descripcion = result.Descripcion,
                Total = detalle.Sum(c => c.Subtotal).ToString("C0"),
                Detalles = detalle.Select(x => new VerRecuentoDetalleVM
                {
                    Nombre = x.Articulo.NombreCompleto,
                    Cantidad = x.Cantidad + "",
                    Precio = x.Precio.ToString("C0"),
                    Subtotal = x.Subtotal.ToString("C0"),
                    Id = x.IdArticulo,
                    Codigo = x.Codigo
                }).ToList()
            };

            return PartialView("_ModalVerRecuento", final);
        }

        [HttpGet]
        public IActionResult ImportarRecuento()
        {
            var model = new ExcelVM
            {
                Url = "/files/ExcelModeloRecuento4.xlsx"
            };

            return PartialView("_ImportarRecuento", model);
        }

        [HttpPost]
        public async Task<IActionResult> ImportarRecuento(ExcelVM model)
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

                var uploads = Path.Combine(hostingEnvironment.WebRootPath, "files\\Recuentos");

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

                    Recuento nuevoRecuento = new Recuento();

                    nuevoRecuento.Nombre = "Recuento Importado " + CurrentDate;
                    nuevoRecuento.Etiquetas = "Importado";
                    nuevoRecuento.IdUsuario = usuarioService.GetByEmail(User.Identity.Name).Id;
                    nuevoRecuento.FechaCreacion = CurrentDate;


                    for (int row = 3; row <= rowCount; row++)
                    {
                        try
                        {
                            if (Convert.ToInt32(worksheet.Cells[row, 4]?.Value) == 0)
                            {
                                continue;
                            }


                            nuevoRecuento.Detalles.Add(new DetalleRecuento
                            {
                                IdArticulo = articuloService.GetSingle(c => c.Id == Convert.ToInt32(worksheet.Cells[row, 4].Value)).Id,
                                Cantidad = Convert.ToInt32(worksheet.Cells[row, 2]?.Value),
                                IdRecuento = recuentoService.GetAll().LastOrDefault().Id + 1,
                                Codigo = articuloService.GetSingle(c => c.Id == Convert.ToInt32(worksheet.Cells[row, 4].Value)).Codigo,
                                Precio = Convert.ToDouble(worksheet.Cells[row, 3].Value),
                                UnidadMedida = articuloService.GetSingle(c => c.Id == Convert.ToInt32(worksheet.Cells[row, 4].Value)).UnidMedida,
                            });

                            //var nuevo = new DetalleRecuento
                            //{
                            //    IdArticulo = articuloService.GetSingle(c => c.Id == (int) worksheet.Cells[row, 4].Value).Id,
                            //    Cantidad = (int)worksheet.Cells[row, 2]?.Value,
                            //    IdRecuento = recuentoService.GetAll().LastOrDefault().Id + 1,
                            //    Codigo = articuloService.GetSingle(c => c.Nombre == worksheet.Cells[row, 1].Value.ToString()).Codigo,
                            //    Precio = (double)worksheet.Cells[row, 3].Value,
                            //    UnidadMedida = articuloService.GetSingle(c => c.Nombre == worksheet.Cells[row, 1].Value.ToString()).UnidMedida,
                            //};

                            //try
                            //{
                            //    detalleRecuentoService.Add(nuevo);
                            //}
                            //catch (Exception e)
                            //{
                            //    errores.Add($"{e.Message} en la fila {row}");
                            //    continue;
                            //}


                            //exitos.Add(.IdArticulo.ToString());
                        }
                        catch (Exception e)
                        {
                            errores.Add($"Algunos valores en la fila {row} son incorrectos");
                            continue;
                        }
                    }

                    recuentoService.Add(nuevoRecuento);
                }

                return Json(new { success = true, message = Valores.Creacion, exitos = exitos.ToArray(), errores = errores.ToArray() });
            }
            catch (System.Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }

        [HttpGet]
        public IActionResult CargarDatosArticulo(int id)
        {
            var final = articuloService.GetById(id);

            return Json(new
            {
                success = true,
                data = new
                {
                    final.Codigo,
                    final.Precio,
                    final.UnidMedida
                }
            });
        }

        [HttpGet]
        public IActionResult ExportarExcelRecuento(int id, string idDetalle = "")
        {
            var nombre = $"computo_{Guid.NewGuid().ToString().Substring(0, 6)}.xlsx";

            var final = Path.Combine("../../files/", nombre);

            var uploads = Path.Combine(hostingEnvironment.WebRootPath, "files");

            var filePath = Path.Combine(uploads, nombre);

            var newFile = new FileInfo(filePath);

            if (newFile.Exists)
            {
                newFile.Delete();

                newFile = new FileInfo(filePath);
            }            

            var row = 9;
            var originalRow = row;

            using (var package = new ExcelPackage(newFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Computo");

                worksheet.Cells[row, 1].Value = "Ítem";
                worksheet.Cells[row, 2].Value = "Código";
                worksheet.Cells[row, 3].Value = "Descripción";
                worksheet.Cells[row, 4].Value = "Marca";
                worksheet.Cells[row, 5].Value = "Unidad de medida";
                worksheet.Cells[row, 6].Value = "Cantidad";
                worksheet.Cells[row, 7].Value = "Observaciones";
                                
                row++;

                var detalles = new List<DetalleRecuento>();

                if(idDetalle != null)
                {
                    var idsArray = idDetalle.Split(',').Select(c => int.Parse(c));

                    id = detalleRecuentoService.GetById(idsArray.First()).IdRecuento;

                    detalles = detalleRecuentoService.GetList(c => idsArray.Contains(c.Id), c => c.Articulo).ToList();
                }
                else
                {
                    detalles = detalleRecuentoService.GetList(c => c.IdRecuento == id, c => c.Articulo).ToList();
                }

                var computo = recuentoService.GetSingle(c => c.Id == id, c => c.Usuario);

                worksheet.Cells[1, 1].Value = "INTELMEC";
                worksheet.Cells[1, 1].Style.Font.Size = 22;
                worksheet.Cells[1, 1].Style.Font.Bold = true;

                worksheet.Cells[3, 1].Value = "Computo: ";
                worksheet.Cells[3, 2].Value = computo.Nombre;
                worksheet.Cells[4, 1].Value = "Tags/Proyecto: ";
                worksheet.Cells[4, 2].Value = computo.Etiquetas;
                worksheet.Cells[5, 1].Value = "Creado por: ";
                worksheet.Cells[5, 2].Value = computo.Usuario.Nombre;
                worksheet.Cells[6, 1].Value = "Creación: ";
                worksheet.Cells[6, 2].Value = computo.FechaCreacion.ToString("dd/MM/yyyy");
                worksheet.Cells[7, 1].Value = "Modificación: ";
                worksheet.Cells[7, 2].Value = computo.FechaCreacion.ToString("dd/MM/yyyy");

                var idsArticulos = detalles.Select(c => c.IdArticulo).Distinct();

                foreach (var item2 in idsArticulos)
                {
                    var detallesItem = detalles.Where(c => c.IdArticulo == item2);

                    worksheet.Cells[row, 1].Value = row - originalRow;
                    worksheet.Cells[row, 2].Value = detallesItem.FirstOrDefault()?.Codigo;
                    worksheet.Cells[row, 3].Value = detallesItem.FirstOrDefault()?.Articulo.NombreCompleto;
                    worksheet.Cells[row, 4].Value = detallesItem.FirstOrDefault()?.Articulo.Marca;
                    worksheet.Cells[row, 5].Value = detallesItem.FirstOrDefault()?.UnidadMedida;
                    worksheet.Cells[row, 6].Value = detallesItem.Sum(c => c.Cantidad);
                    worksheet.Cells[row, 7].Value = null;

                    row++;
                }

                for (int i = 1; i <= 7; i++)
                {
                    worksheet.Column(i).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    worksheet.Column(i).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet.Cells[originalRow, i].Style.Border.BorderAround(ExcelBorderStyle.Thick);
                    worksheet.Cells[originalRow, i].Style.Font.Bold = true;
                    worksheet.Cells[originalRow, i].Style.Fill.SetBackground(OfficeOpenXml.Drawing.eThemeSchemeColor.Background2);

                    for (int j = originalRow; j <= originalRow + idsArticulos.Count(); j++)
                    {
                        worksheet.Cells[j, i].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                        worksheet.Cells[j, i].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                        if (j == originalRow + idsArticulos.Count())
                        {
                            worksheet.Cells[j, i].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                        }
                    }
                }

                //for (int i = 1; i <= originalRow; i++)
                //{
                //    worksheet.Row(i).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                //    worksheet.Row(i).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                //}

                for (int i = 3; i <= 7; i++)
                {
                    for (int j = 1; j <= 2; j++)
                    {
                        worksheet.Cells[i, j].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        worksheet.Cells[i, j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        worksheet.Cells[i, j].Style.Border.BorderAround(ExcelBorderStyle.Medium);

                        if (j == 1)
                        {
                            worksheet.Cells[i, j].Style.Font.Bold = true;
                        }
                    }

                }

                worksheet.Cells.AutoFitColumns(20, 100);

                package.Save();
            }

           return Json(new { success = true, file = final.Replace("../..", "") });
        }

        [HttpGet]
        public IActionResult ExportarNuevo(int id, string idDetalle = "")
        {
            return PartialView("_ModalExportar", new ExportarRecuentoVM
            {
                IdDetalles = idDetalle,
                IdRecuento = id,
                Coeficiente = 1,
                CodigoProveedor = false
            });
        }

        [HttpPost]
        public IActionResult ExportarNuevo(ExportarRecuentoVM model)
        {
            var nombre = $"computo_{Guid.NewGuid().ToString().Substring(0, 6)}.xlsx";

            var final = Path.Combine("../../files/", nombre);

            var uploads = Path.Combine(hostingEnvironment.WebRootPath, "files");

            var filePath = Path.Combine(uploads, nombre);

            var newFile = new FileInfo(filePath);

            if (newFile.Exists)
            {
                newFile.Delete();

                newFile = new FileInfo(filePath);
            }

            var row = 9;
            var originalRow = row;

            var id = model.IdRecuento;
            var idDetalle = model.IdDetalles;

            using (var package = new ExcelPackage(newFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Computo");

                worksheet.Cells[row, 1].Value = "Ítem";
                worksheet.Cells[row, 2].Value = "Código";
                worksheet.Cells[row, 3].Value = "Descripción";
                worksheet.Cells[row, 4].Value = "Marca";
                worksheet.Cells[row, 5].Value = "Unidad de medida";
                worksheet.Cells[row, 6].Value = "Cantidad";
                worksheet.Cells[row, 7].Value = "Observaciones";

                row++;

                var detalles = new List<DetalleRecuento>();

                if (!string.IsNullOrEmpty(idDetalle))
                {
                    var idsArray = idDetalle.Split(',').Select(c => int.Parse(c));

                    id = detalleRecuentoService.GetById(idsArray.First()).IdRecuento;

                    detalles = detalleRecuentoService.GetList(c => idsArray.Contains(c.Id), c => c.Articulo).ToList();
                }
                else
                {
                    detalles = detalleRecuentoService.GetList(c => c.IdRecuento == id, c => c.Articulo).ToList();
                }

                var articulos = detalles.Select(c => c.IdArticulo).Distinct().ToArray();

                var codigos = codigoProveedorService.GetList(c => c.IdProveedor == (int)ProveedoresEnum.Richetta && articulos.Contains(c.IdArticulo));

                var computo = recuentoService.GetSingle(c => c.Id == id, c => c.Usuario);

                worksheet.Cells[1, 1].Value = "INTELMEC";
                worksheet.Cells[1, 1].Style.Font.Size = 22;
                worksheet.Cells[1, 1].Style.Font.Bold = true;

                worksheet.Cells[3, 1].Value = "Computo: ";
                worksheet.Cells[3, 2].Value = computo.Nombre;
                worksheet.Cells[4, 1].Value = "Tags/Proyecto: ";
                worksheet.Cells[4, 2].Value = computo.Etiquetas;
                worksheet.Cells[5, 1].Value = "Creado por: ";
                worksheet.Cells[5, 2].Value = computo.Usuario.Nombre;
                worksheet.Cells[6, 1].Value = "Creación: ";
                worksheet.Cells[6, 2].Value = computo.FechaCreacion.ToString("dd/MM/yyyy");
                worksheet.Cells[7, 1].Value = "Modificación: ";
                worksheet.Cells[7, 2].Value = computo.FechaCreacion.ToString("dd/MM/yyyy");

                var idsArticulos = detalles.Select(c => c.IdArticulo).Distinct();

                foreach (var item2 in idsArticulos)
                {
                    var detallesItem = detalles.Where(c => c.IdArticulo == item2);

                    worksheet.Cells[row, 1].Value = row - originalRow;
                    worksheet.Cells[row, 2].Value = model.CodigoProveedor 
                        ? (codigos.FirstOrDefault(c => c.IdArticulo == item2)?.Codigo ?? detallesItem.FirstOrDefault()?.Codigo)
                        : detallesItem.FirstOrDefault()?.Codigo;
                    worksheet.Cells[row, 3].Value = detallesItem.FirstOrDefault()?.Articulo.NombreCompleto;
                    worksheet.Cells[row, 4].Value = detallesItem.FirstOrDefault()?.Articulo.Marca;
                    worksheet.Cells[row, 5].Value = detallesItem.FirstOrDefault()?.UnidadMedida;
                    worksheet.Cells[row, 6].Value = detallesItem.Sum(c => c.Cantidad) * model.Coeficiente;
                    worksheet.Cells[row, 7].Value = null;

                    row++;
                }

                for (int i = 1; i <= 7; i++)
                {
                    worksheet.Column(i).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    worksheet.Column(i).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet.Cells[originalRow, i].Style.Border.BorderAround(ExcelBorderStyle.Thick);
                    worksheet.Cells[originalRow, i].Style.Font.Bold = true;
                    worksheet.Cells[originalRow, i].Style.Fill.SetBackground(OfficeOpenXml.Drawing.eThemeSchemeColor.Background2);

                    for (int j = originalRow; j <= originalRow + idsArticulos.Count(); j++)
                    {
                        worksheet.Cells[j, i].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                        worksheet.Cells[j, i].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                        if (j == originalRow + idsArticulos.Count())
                        {
                            worksheet.Cells[j, i].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                        }
                    }
                }

                //for (int i = 1; i <= originalRow; i++)
                //{
                //    worksheet.Row(i).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                //    worksheet.Row(i).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                //}

                for (int i = 3; i <= 7; i++)
                {
                    for (int j = 1; j <= 2; j++)
                    {
                        worksheet.Cells[i, j].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        worksheet.Cells[i, j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        worksheet.Cells[i, j].Style.Border.BorderAround(ExcelBorderStyle.Medium);

                        if (j == 1)
                        {
                            worksheet.Cells[i, j].Style.Font.Bold = true;
                        }
                    }

                }

                worksheet.Cells.AutoFitColumns(20, 100);

                package.Save();
            }

            return Json(new { success = true, file = final.Replace("../..", "") });
        }
        #endregion

        #region CODIGOS
        [HttpGet]
        public IActionResult CodigoProveedor()
        {
            ViewData["Title"] = "CodigoProveedor";

            return View();
        }

        [HttpGet]
        public IActionResult CargarTablaCodigoProveedor()
        {
            var final = CargarCodigoProveedor(null);

            return Json(new { success = true, data = final });
        }

        private IEnumerable<object> CargarCodigoProveedor(int? id)
        {
            return codigoProveedorService.GetList(c => (id == null ? true : c.Id == id), c => c.Proveedor, c => c.Articulo).
                AsEnumerable().Select(c => new
                {
                    id = c.Id,
                    idProveedor = c.IdProveedor,
                    proveedor = c.Proveedor.Alias,
                    idArticulo = c.IdArticulo,
                    articulo = c.Articulo.Nombre,
                    codigoGeneral = c.Articulo.Codigo,
                    codigo = c.Codigo,
                    precio = c.PrecioProveedor.ToString("C1"),

                })
                .OrderBy(c => c.articulo);
        }

        //LO GUARDO PARA USARLO EN EL OTRO METODO
        //private IEnumerable<object> CargarCodigoProveedor(int? idProveedor)
        //{
        //    return codigoProveedorService.GetList(c => (idProveedor == null ? true : c.IdProveedor == idProveedor), c => c.Proveedor, c => c.Articulo).
        //        AsEnumerable().Select(c => new
        //        {
        //            id = c.Id,
        //            //idProveedor = c.IdProveedor,
        //            proveedor = c.Proveedor.Alias,
        //            //idArticulo = c.IdArticulo,
        //            articulo = c.Articulo.Nombre,
        //            codigoGeneral = c.Articulo.Codigo,
        //            codigo = c.Codigo,
        //            precio = c.PrecioProveedor.ToString("C1"),

        //        })
        //        .OrderBy(c => c.articulo);
        //}

        [HttpGet]
        public IActionResult CreateCodigoProveedor(int id)
        {
            ViewBag.IdProveedor = new SelectList(proveedorService.GetAll(), "Id", "Alias");

            ViewBag.IdArticulo = new SelectList(articuloService.GetAll(), "Id", "NombreCompleto");

            return PartialView("_ModalCodigoProveedor", new CodigoProveedor
            {

            });
        }

        [HttpPost]
        public IActionResult CreateCodigoProveedor(CodigoProveedor model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = Valores.Incorrectos });
            }

            codigoProveedorService.Add(model);

            var final = CargarCodigoProveedor(model.Id).First();

            return Json(new { success = true, data = final, message = Valores.Creacion });
        }

        [HttpGet]
        public IActionResult EditCodigoProveedor(int id)
        {
            var result = codigoProveedorService.GetById(id);

            ViewBag.IdProveedor = new SelectList(proveedorService.GetAll(), "Id", "Alias", result.IdProveedor);

            ViewBag.IdArticulo = new SelectList(articuloService.GetAll(), "Id", "NombreCompleto", result.IdArticulo);

            return PartialView("_ModalCodigoProveedor", result);
        }

        [HttpPost]
        public IActionResult EditCodigoProveedor(CodigoProveedor model)
        {
            ModelState.Remove("Id");

            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = Valores.Incorrectos });
            }

            var codigoProveedor = codigoProveedorService.GetById(model.Id);

            codigoProveedor.IdProveedor = model.IdProveedor;
            codigoProveedor.IdArticulo = model.IdArticulo;
            codigoProveedor.Codigo = model.Codigo;
            codigoProveedor.PrecioProveedor = model.PrecioProveedor;

            codigoProveedorService.Edit(codigoProveedor);

            var final = CargarCodigoProveedor(model.Id).First();

            return Json(new { success = true, data = final, message = Valores.Edicion });
        }

        [HttpGet]
        public IActionResult EditInlineCodigoProveedor(int id)
        {
            var result = codigoProveedorService.GetList(c => c.IdArticulo == id, c => c.Proveedor, c => c.Articulo)
                .Select(c => new EditCodigoProveedorVM
                {
                    Id = c.Id,
                    Articulo = c.Articulo.Nombre,
                    CodigoGral = c.Articulo.Codigo,
                    CodigoProveedor = c.Codigo,
                    Precio = c.PrecioProveedor,
                    Proveedor = c.Proveedor.Alias
                });

            return PartialView("_ModalCodigoProveedorInline", result);
        }

        public IActionResult EditInlineCodigo(int id, string valor, string campo)
        {
            var model = codigoProveedorService.GetById(id);

            if (model == null)
            {
                return Json(new { Resultado = false, Mensaje = "Codigo Proveedor no encontrado" });
            }

            switch (campo)
            {
                case "codigo":
                    model.Codigo = valor;
                    codigoProveedorService.Edit(model);
                    return Json(new
                    {
                        Resultado = true,
                        Mensaje = "Éxito!",
                    });
                case "precio":
                    model.PrecioProveedor = double.Parse(valor);
                    codigoProveedorService.Edit(model);
                    return Json(new
                    {
                        Resultado = true,
                        Mensaje = "Exito!",
                    });
                default:
                    break;
            }

            return Json(new
            {
                Resultado = false,
                Mensaje = "Hubo un error al modificar el valor",
            });
        }

        //public IActionResult ActivarArticulo(int id)
        //{
        //    var model = articuloService.GetById(id);

        //    model.Activo = !model.Activo;

        //    articuloService.Edit(model);

        //    var final = CargarArticulos(model.Id).First();

        //    return Json(new { success = true, data = final, message = Enum.Valores.Edicion });
        //}

        public IActionResult EliminarCodigoProveedor(int id)
        {
            var model = codigoProveedorService.GetById(id);

            codigoProveedorService.Delete(model);

            var final = CargarCodigoProveedor(id);

            return Json(new { success = true, data = final, message = Enum.Valores.Eliminacion });
        }

        [HttpGet]
        public IActionResult ImportarCodigoProveedor()
        {
            var model = new ExcelVM
            {
                Url = "/files/ExcelModeloCodigoProveedor.xlsx"
            };

            return PartialView("_ImportarCodigoProveedor", model);
        }

        [HttpPost]
        public async Task<IActionResult> ImportarCodigoProveedor(ExcelVM model)
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

                            var nuevo = new CodigoProveedor
                            {
                                //IdProveedor = codigoProveedorService.GetList(c => c.Proveedor.Alias.Equals(worksheet.Cells[row, 1].Value), c => c.Proveedor).Select
                                //(c => new
                                //{
                                //   Id
                                //}).FirstOrDefault(),

                                IdProveedor = codigoProveedorService.GetList(c => c.Proveedor.Alias.Equals(worksheet.Cells[row, 1].Value), c => c.Proveedor).FirstOrDefault().IdProveedor,
                                //IdProveedor = (int) worksheet.Cells[row, 1]?.Value,
                                IdArticulo = codigoProveedorService.GetList(c => c.Articulo.Nombre.Equals(worksheet.Cells[row, 2].Value), c => c.Articulo).FirstOrDefault().IdArticulo,
                                //IdArticulo = (int) worksheet.Cells[row, 2]?.Value,
                                Codigo = worksheet.Cells[row, 3]?.Value.ToString(),
                                PrecioProveedor = (double)worksheet.Cells[row, 4]?.Value,
                            };

                            try
                            {
                                codigoProveedorService.Add(nuevo);
                            }
                            catch (Exception e)
                            {
                                errores.Add($"{e.Message} en la fila {row}");
                                continue;
                            }

                            exitos.Add(nuevo.IdArticulo.ToString());
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