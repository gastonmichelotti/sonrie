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
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;

namespace netCoreNew.Controllers
{
    [Authorize]
    public class AdminController : BaseController
    {
        private readonly IUsuarioService usuarioService;
        private readonly IArticuloService articuloService;
        private readonly IRolService rolService;
        private readonly IWebHostEnvironment hostingEnvironment;


        public AdminController(
            IUsuarioService usuarioService,
            IArticuloService articuloService,
            IRolService rolService,
            IWebHostEnvironment hostingEnvironment)
        {
            this.usuarioService = usuarioService;
            this.articuloService = articuloService;
            this.rolService = rolService;
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
            return articuloService.GetList(c => (id == null ? !c.Eliminado : c.Id == id))
                .Select(c => new
                {
                    id = c.Id,
                    nombre = c.Nombre,
                    activo = c.Activo,
                    codigo = c.Codigo,
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
        #endregion
    }
}