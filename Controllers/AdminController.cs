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
using Newtonsoft.Json;
using static netCoreNew.Enum.Valores;

namespace netCoreNew.Controllers
{
    [Authorize]
    public class AdminController : BaseController
    {
        private readonly IUsuarioService usuarioService;        
        private readonly IInsumoxCategoriaService insumoxCategoriaService;
        private readonly IRolService rolService;        
        private readonly IPrestacionxAtencionService prestacionxAtencionService;        
        private readonly IPrestacionService prestacionService;        
        private readonly IPrecioService precioService;        
        private readonly IPacienteService pacienteService;        
        private readonly IObraSocialService obraSocialService;        
        private readonly IInsumoService insumoService;        
        private readonly ICategoriaPrestacionService categoriaPrestacionService;        
        private readonly IAtencionService atencionService;        
        private readonly IDolarService dolarService;        
        private readonly IWebHostEnvironment hostingEnvironment;


        public AdminController(
            IUsuarioService usuarioService,            
            IInsumoxCategoriaService insumoxCategoriaService,
            IRolService rolService,
            IPrestacionxAtencionService prestacionxAtencionService,
            IPrestacionService prestacionService,
            IPrecioService precioService,
            IPacienteService pacienteService,
            IObraSocialService obraSocialService,
            IInsumoService insumoService,
            ICategoriaPrestacionService categoriaPrestacionService,
            IAtencionService atencionService,
            IDolarService dolarService,
            IWebHostEnvironment hostingEnvironment)
        {
            this.usuarioService = usuarioService;           
            this.insumoxCategoriaService = insumoxCategoriaService;
            this.rolService = rolService;  
            this.prestacionxAtencionService = prestacionxAtencionService;  
            this.prestacionService = prestacionService;  
            this.precioService = precioService;  
            this.pacienteService = pacienteService;  
            this.obraSocialService = obraSocialService;  
            this.insumoService = insumoService;  
            this.categoriaPrestacionService = categoriaPrestacionService;  
            this.atencionService = atencionService;              
            this.dolarService = dolarService;              
            this.hostingEnvironment = hostingEnvironment;
        }

        #region DOLAR_Y_GENERALES

        [HttpGet]
        public IActionResult Dolar()
        {
            var result = dolarService.GetAll().LastOrDefault();

            return PartialView("_ModalDolar", result);
        }

        [HttpPost]
        public IActionResult Dolar(Dolar model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = Valores.Incorrectos });
            }

            model.Id = 0;
            model.Fecha = CurrentDate;

            dolarService.Add(model);

            return Json(new { success = true, message = Valores.Edicion });
        }

        #endregion

        #region ROLES
        [HttpGet]
        public IActionResult Roles()
        {
            ViewData["Title"] = "Roles";

            return View();
        }

        [HttpGet]
        public IActionResult CargarTablaRoles()
        {
            var final = CargarRoles(null);

            return Json(new { success = true, data = final });
        }

        private IEnumerable<object> CargarRoles(int? id)
        {
            return rolService.GetList(c => (id == null ? !c.Eliminado : c.Id == id))
                .Select(c => new
                {
                    id = c.Id,                    
                    nombre = c.Nombre,
                    redirect = c.Redirect,
                    observaciones = c.Observaciones
                })
                .OrderBy(c => c.nombre);
        }

        [HttpGet]
        public IActionResult CreateRol(int id)
        {
            return PartialView("_ModalRol", new Rol
            {

            });
        }
        
        [HttpPost]
        public IActionResult CreateRol(Rol model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = Valores.Incorrectos });
            }

            model.Eliminado = false;

            rolService.Add(model);

            var final = CargarRoles(model.Id).First();

            return Json(new { success = true, data = final, message = Valores.Creacion });
        }

        [HttpGet]
        public IActionResult EditRol(int id)
        {
            var result = rolService.GetById(id);

            return PartialView("_ModalRol", result);
        }

        [HttpPost]
        public IActionResult EditRol(Rol model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = Valores.Incorrectos });
            }

            var rol = rolService.GetById(model.Id);

            rol.Nombre = model.Nombre;
            rol.Redirect = model.Redirect;
            rol.Observaciones = model.Observaciones;

            rolService.Edit(rol);

            var final = CargarRoles(model.Id).First();

            return Json(new { success = true, data = final, message = Valores.Edicion });
        }


        public IActionResult EliminarRol(int id)
        {
            var model = rolService.GetById(id);

            model.Eliminado = true;

            rolService.Edit(model);

            return Json(new { success = true, message = Enum.Valores.Eliminacion });
        }
        #endregion

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

        #region PACIENTES
        [HttpGet]
        public IActionResult Pacientes()
        {
            ViewData["Title"] = "Pacientes";

            return View();
        }

        [HttpGet]
        public IActionResult CargarTablaPacientes()
        {
            var final = CargarPacientes(null);

            return Json(new { success = true, data = final });
        }

        private IEnumerable<object> CargarPacientes(int? id)
        {
            return pacienteService.GetList(c => (id == null ? !c.Eliminado : c.Id == id), c => c.ObraSocial)
                .Select(c => new
                {   
                    id = c.Id,
                    nombre = c.Nombre,                    
                    apellido = c.Apellido,                    
                    telefono = c.Telefono,
                    obraSocial = c.ObraSocial.Nombre,
                    plan =  c.OsPlan,
                    numAfiliado = c.NumAfiliado
                })
                .OrderBy(c => c.apellido);
        }

        [HttpGet]
        public IActionResult CreatePaciente(int id)
        {
            ViewBag.IdObraSocial = new SelectList(obraSocialService.GetList(c => c.Activo), "Id", "Nombre");

            return PartialView("_ModalPaciente", new Paciente
            {

            });
        }

        [HttpPost]
        public IActionResult CreatePaciente(Paciente model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = Valores.Incorrectos });
            }

            model.FechaAltaPaciente = CurrentDate;             

            pacienteService.Add(model);

            var final = CargarPacientes(model.Id).First();

            return Json(new { success = true, data = final, message = Valores.Creacion });
        }

        [HttpGet]
        public IActionResult EditPaciente(int id)
        {
            var result = pacienteService.GetById(id);

            ViewBag.IdObraSocial = new SelectList(obraSocialService.GetList(c => c.Activo) , "Id", "Nombre", result.IdObraSocial);

            return PartialView("_ModalPaciente", result);
        }

        [HttpPost]
        public IActionResult EditPaciente(Paciente model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = Valores.Incorrectos });
            }

            var paciente = pacienteService.GetById(model.Id);

            paciente.Nombre = model.Nombre;
            paciente.Apellido = model.Nombre;
            paciente.Telefono = model.Telefono;
            paciente.Mail = model.Mail;
            paciente.Dni = model.Dni;
            paciente.IdObraSocial = model.IdObraSocial;
            paciente.OsPlan = model.OsPlan;
            paciente.NumAfiliado = model.NumAfiliado;
            paciente.Eliminado = false;

            pacienteService.Edit(paciente);

            var final = CargarPacientes(model.Id).First();

            return Json(new { success = true, data = final, message = Valores.Edicion });
        }

        //public IActionResult ActivarUsuario(int id)
        //{
        //    var model = usuarioService.GetById(id);

        //    model.Activo = !model.Activo;

        //    usuarioService.Edit(model);

        //    var final = CargarUsuarios(model.Id).First();

        //    return Json(new { success = true, data = final, message = Enum.Valores.Edicion });
        //}

        public IActionResult EliminarPaciente(int id)
        {
            var model = pacienteService.GetById(id);     
            
            model.Eliminado = true;

            pacienteService.Edit(model);

            return Json(new { success = true, message = Enum.Valores.Eliminacion });
        }
        #endregion

        #region OBRAS_SOCIALES
        [HttpGet]
        public IActionResult ObrasSociales()
        {
            ViewData["Title"] = "Obras Sociales";

            return View();
        }

        [HttpGet]
        public IActionResult CargarTablaObrasSociales()
        {
            var final = CargarObrasSociales(null);

            return Json(new { success = true, data = final });
        }

        private IEnumerable<object> CargarObrasSociales(int? id)
        {
            return obraSocialService.GetList(c => (id == null ? c.Activo : c.Id == id))
                .Select(c => new
                {
                    id = c.Id,
                    nombre = c.Nombre,
                    demora = c.DemoraPago,                   
                    observaciones = c.Observaciones,
                    activo = c.Activo
                })
                .OrderBy(c => c.nombre);
        }

        [HttpGet]
        public IActionResult CreateObraSocial(int id)
        {
            ViewBag.IdObraSocial = new SelectList(obraSocialService.GetList(c => c.Activo), "Id", "Nombre");

            return PartialView("_ModalObraSocial", new ObraSocial
            {

            });
        }

        [HttpPost]
        public IActionResult CreateObraSocial(ObraSocial model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = Valores.Incorrectos });
            }

            model.Activo = true;

            obraSocialService.Add(model);

            var prestaciones = prestacionService.GetList(c => !c.Eliminado).ToList();

            foreach (var prestacion in prestaciones)
            {
                precioService.Add(new Precio(prestacion.Id, model.Id, 0, 0));
            }            

            var final = CargarObrasSociales(model.Id).First();

            return Json(new { success = true, data = final, message = Valores.Creacion });
        }

        [HttpGet]
        public IActionResult EditObraSocial(int id)
        {
            var result = obraSocialService.GetById(id);

            ViewBag.IdObraSocial = new SelectList(obraSocialService.GetList(c => c.Activo), "Id", "Nombre", result.Id);

            return PartialView("_ModalObraSocial", result);
        }

        [HttpPost]
        public IActionResult EditObraSocial(ObraSocial model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = Valores.Incorrectos });
            }

            var obraSocial = obraSocialService.GetById(model.Id);

            obraSocial.Nombre = model.Nombre;
            obraSocial.DemoraPago = model.DemoraPago;
            obraSocial.Observaciones = model.Observaciones;
            obraSocial.Activo = true;
            
            ////////////obraSocialService.Edit(obraSocial);

            var final = CargarObrasSociales(model.Id).First();

            return Json(new { success = true, data = final, message = Valores.Edicion });
        }

        public IActionResult ActivarObraSocial(int id)
        {
            var model = obraSocialService.GetById(id);

            model.Activo = !model.Activo;

            obraSocialService.Edit(model);

            var final = CargarObrasSociales(model.Id).First();

            return Json(new { success = true, data = final, message = Enum.Valores.Edicion });
        }

        [HttpGet]
        public IActionResult EditInlinePreciosOS(int id)
        {
            var result = precioService.GetList(c => c.IdObraSocial == id, c => c.ObraSocial, c => c.Prestacion)
                .Select(c => new EditPreciosVM
                {
                    Id = c.Id,
                    Prestacion = c.Prestacion.Codigo +  " - " + c.Prestacion.Nombre,
                    ObraSocial = c.ObraSocial.Nombre,
                    PrecioPesos = c.PrecioPesos,
                    CoseguroPesos = c.CoseguroPesos,
                });

            return PartialView("_ModalPreciosInlineOS", result);
        }


        public IActionResult EditInlinePreciosOSPOST(int id, string valor, string campo)
        {
            var model = precioService.GetById(id);

            if (model == null)
            {
                return Json(new { Resultado = false, Mensaje = "Precios no encontrados" });
            }

            switch (campo)
            {
                case "precioPesos":
                    model.PrecioPesos = double.Parse(valor);
                    precioService.Edit(model);
                    return Json(new
                    {
                        Resultado = true,
                        Mensaje = "Éxito!",
                    });
                case "coseguroPesos":
                    model.CoseguroPesos = double.Parse(valor);
                    precioService.Edit(model);
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


        #endregion

        #region INSUMOS
        [HttpGet]
        public IActionResult Insumos()
        {
            ViewData["Title"] = "Insumos";

            return View();
        }

        [HttpGet]
        public IActionResult CargarTablaInsumos()
        {
            var final = CargarInsumos(null);

            return Json(new { success = true, data = final });
        }

        private IEnumerable<object> CargarInsumos(int? id)
        {
            return insumoService.GetList(c => (id == null ? !c.Eliminado : c.Id == id))
                .Select(c => new
                {
                    id = c.Id,
                    nombre = c.Nombre,
                    unidad = c.UnidadVenta,
                    precioUSD = "USD " + c.PrecioDolar.ToString("N2"),
                    etiquetas = c.Etiquetas,
                    observaciones = c.Observaciones
                   
                })
                .OrderBy(c => c.nombre);
        }

        [HttpGet]
        public IActionResult CreateInsumo(int id)
        {
            
            return PartialView("_ModalInsumo", new Insumo
            {

            });
        }

        [HttpPost]
        public IActionResult CreateInsumo(Insumo model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = Valores.Incorrectos });
            }

            model.Eliminado = false;

            insumoService.Add(model);

            var final = CargarInsumos(model.Id).First();

            return Json(new { success = true, data = final, message = Valores.Creacion });
        }

        [HttpGet]
        public IActionResult EditInsumo(int id)
        {
            var result = insumoService.GetById(id);            

            return PartialView("_ModalInsumo", result);
        }

        [HttpPost]
        public IActionResult EditInsumo(Insumo model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = Valores.Incorrectos });
            }

            var insumo = insumoService.GetById(model.Id);

            insumo.Nombre = model.Nombre;
            insumo.UnidadVenta = model.UnidadVenta;
            insumo.PrecioDolar = model.PrecioDolar;
            insumo.Observaciones = model.Observaciones;
            insumo.Etiquetas = model.Etiquetas;
            insumo.Eliminado = false;

            insumoService.Edit(insumo);

            var final = CargarInsumos(model.Id).First();

            return Json(new { success = true, data = final, message = Valores.Edicion });
        }

        public IActionResult EliminarInsumo(int id)
        {
            var model = insumoService.GetById(id);

            model.Eliminado = true;

            insumoService.Edit(model);

            return Json(new { success = true, message = Enum.Valores.Eliminacion });
        }

        #endregion

        #region ATENCIONES
        [HttpGet]
        public IActionResult Atenciones()
        {
            ViewData["Title"] = "Atenciones";

            return View();
        }

        [HttpGet]
        public IActionResult CargarTablaAtenciones()
        {
            var final = CargarAtenciones(null);

            return Json(new { success = true, data = final });
        }

        private IEnumerable<object> CargarAtenciones(int? id)
        {
            return atencionService.GetList(c => (id == null ? !c.Eliminado : c.Id == id), c => c.Paciente, c => c.Usuario, c => c.EstadoAtencion)
                .Select(c => new
                {
                    id = c.Id,
                    fecha = c.Fecha.ToShortDateString(),                                        
                    paciente = c.Paciente.Nombre + " " + c.Paciente.Apellido,
                    profesional = c.Usuario.Nombre,
                    montoOS = c.MontoOS.ToString("C2"),
                    montoEFT = c.MontoEfectivo.ToString("C2"),
                    estado = $"<span class='text-{c.EstadoAtencion.Color}'>{c.EstadoAtencion.Nombre}</ span > ",
                    observaciones = c.Observaciones,

                })
                .OrderBy(c => c.fecha);
        }

        [HttpGet]
        public IActionResult CreateAtencion(int id)
        {   

            ViewBag.IdPaciente = new SelectList(pacienteService.GetList(c => !c.Eliminado).Select(c => new
            {
                Id = c.Id,
                Nombre = c.Nombre + " " + c.Apellido
            }), "Id", "Nombre"); 
            ViewBag.IdPrestacion = new SelectList(prestacionService.GetList(c => !c.Eliminado), "Id", "Nombre");
            ViewBag.IdUsuario = new SelectList(usuarioService.GetList(c => c.Activo), "Id", "Nombre");
            ViewBag.IdPieza = new SelectList(System.Enum.GetValues(typeof(Valores.PiezaEnum)));
            ViewBag.IdEstadoAtencion = new SelectList(System.Enum.GetValues(typeof(Valores.EstadoAtencionEnum)));
            ViewBag.IdFormadePago = new SelectList(System.Enum.GetValues(typeof(Valores.FormaDePagoEnum)));

            return PartialView("_ModalAtencion", new Atencion
            {
                FechaString = CurrentDate.ToString("dd/MM/yyyy"),
                IdUsuario = usuarioService.GetByEmail(User.Identity.Name).Id
            }) ;
        }

        [HttpPost]
        public IActionResult CreateAtencion(Atencion model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = Valores.Incorrectos });
            }

            var prestaciones = prestacionService.GetList(c => !c.Eliminado, c=> c.Precios).ToArray();
            var obraSocialPaciente = pacienteService.GetById(model.IdPaciente).IdObraSocial;

            foreach (var item in model.Items)
            {
                model.Detalles.Add(new PrestacionxAtencion
                {
                    IdPrestacion = item.IdPrestacion,
                    IdPieza = item.IdPieza,
                    Caras = item.Caras,
                    Particular = item.Particular,
                    Observaciones = item.Observaciones

                });
                //TODO: ARMAR RULO DE TOMAR PRECIOS PARTICULARES IF CHECJBOX PARTICULAR == TRUE
                model.MontoEfectivo += prestaciones.Where(y => y.Id == item.IdPrestacion).FirstOrDefault().Precios.Where(c => c.IdObraSocial == obraSocialPaciente).Sum(x => x.CoseguroPesos);
                model.MontoOS += prestaciones.Where(y => y.Id == item.IdPrestacion).FirstOrDefault().Precios.Where(c => c.IdObraSocial == obraSocialPaciente).Sum(x => x.PrecioPesos);
                model.Fecha = DateTime.ParseExact(model.FechaString, "dd/MM/yyyy", null);
            }           

            atencionService.Add(model);

            var final = CargarAtenciones(model.Id).First();

            return Json(new { success = true, data = final, message = Valores.Creacion });
                }

        [HttpGet]
        public IActionResult EditAtencion(int id)
        {
            ViewBag.IdPaciente = new SelectList(pacienteService.GetList(c => !c.Eliminado).Select(c => new
            {
                Id = c.Id,
                Nombre = c.Nombre + " " + c.Apellido
            }), "Id", "Nombre");    
            ViewBag.IdPrestacion = new SelectList(prestacionService.GetList(c => !c.Eliminado), "Id", "Nombre");
            ViewBag.IdUsuario = new SelectList(usuarioService.GetList(c => c.Activo), "Id", "Nombre");
            ViewBag.IdPieza = new SelectList(System.Enum.GetValues(typeof(Valores.PiezaEnum)));
            ViewBag.IdEstadoAtencion = new SelectList(System.Enum.GetValues(typeof(Valores.EstadoAtencionEnum)));
            ViewBag.IdFormadePago = new SelectList(System.Enum.GetValues(typeof(Valores.FormaDePagoEnum)));


            var result = atencionService.GetById(id);                       

            result.ItemsLoad = JsonConvert.SerializeObject(prestacionxAtencionService.GetList(c => c.IdAtencion == id).Select(c => new
            {
                IdPrestacion = c.IdPrestacion,
                IdPieza = c.IdPieza,
                Caras = c.Caras,
                Particular = c.Particular,
                Observaciones = c.Observaciones
            }));

            return PartialView("_ModalAtencion", result);
        }

        [HttpPost]
        public IActionResult EditAtencion(Atencion model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = Valores.Incorrectos });
            }

            var atencion = atencionService.GetById(model.Id);

            var detalles = prestacionxAtencionService.GetList(c => c.IdAtencion == model.Id);

            prestacionxAtencionService.DeleteRange(detalles.ToArray());

            atencion.MontoOS = 0;

            atencion.MontoEfectivo = 0;

            var prestaciones = prestacionService.GetList(c => !c.Eliminado, c => c.Precios).ToArray();
            var obraSocialPaciente = pacienteService.GetById(model.IdPaciente).IdObraSocial;

            foreach (var item in model.Items)
            {
                atencion.Detalles.Add(new PrestacionxAtencion
                {
                    IdPrestacion = item.IdPrestacion,
                    IdPieza = item.IdPieza,
                    Caras = item.Caras,
                    Particular = item.Particular,
                    Observaciones = item.Observaciones
                });

                //TODO: ARMAR RULO DE TOMAR PRECIOS PARTICULARES IF CHECJBOX PARTICULAR == TRUE
                atencion.MontoEfectivo += prestaciones.Where(y => y.Id == item.IdPrestacion).FirstOrDefault().Precios.Where(c => c.IdObraSocial == obraSocialPaciente).Sum(x => x.CoseguroPesos);
                atencion.MontoOS += prestaciones.Where(y => y.Id == item.IdPrestacion).FirstOrDefault().Precios.Where(c => c.IdObraSocial == obraSocialPaciente).Sum(x => x.PrecioPesos);
    
            }

            atencion.Fecha = model.Fecha;
            atencion.IdPaciente = model.IdPaciente;
            atencion.IdFormadePago = model.IdFormadePago;
            atencion.IdUsuario = model.IdUsuario;
            atencion.IdEstadoAtencion = model.IdEstadoAtencion;
            atencion.Fecha = DateTime.ParseExact(model.FechaString, "dd/MM/yyyy", null);

            atencionService.Edit(atencion);

            var final = CargarAtenciones(model.Id).First();

            return Json(new { success = true, data = final, message = Valores.Edicion });
        }

        public IActionResult EliminarAtencion(int id)
        {
            var model = atencionService.GetById(id);

            model.Eliminado = true;

            atencionService.Edit(model);

            return Json(new { success = true, message = Enum.Valores.Eliminacion });
        }

        #endregion

        #region PRESTACIONES
        [HttpGet]
        public IActionResult Prestaciones()
        {
            ViewData["Title"] = "Prestaciones";

            return View();
        }

        [HttpGet]
        public IActionResult CargarTablaPrestaciones()
        {
            var final = CargarPrestaciones(null);

            return Json(new { success = true, data = final });
        }

        private IEnumerable<object> CargarPrestaciones(int? id)
        {
            return prestacionService.GetList(c => (id == null ? !c.Eliminado : c.Id == id), c => c.CategoriaPrestacion, c => c.Precios)
                .Select(c => new
                {
                    id = c.Id,
                    codigo = c.Codigo,
                    nombre = c.Nombre,
                    categoria = c.CategoriaPrestacion.Nombre,                                        
                    observaciones = c.Observaciones,                   
                })
                .OrderBy(c => c.nombre);
        }

        [HttpGet]
        public IActionResult CreatePrestacion(int id)
        {
            ViewBag.IdCategoriaPrestacion = new SelectList(categoriaPrestacionService.GetList(c => !c.Eliminado), "Id", "Nombre");

            return PartialView("_ModalPrestacion", new Prestacion
            {

            });
        }

        [HttpPost]
        public IActionResult CreatePrestacion(Prestacion model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = Valores.Incorrectos });
            }

            model.Eliminado = false;
            
            prestacionService.Add(model);

            var obrasSociales = obraSocialService.GetList(c => c.Activo).ToList();

            foreach (var obraSocial in obrasSociales)
            {
                precioService.Add(new Precio(model.Id, obraSocial.Id, 0, 0));
            }

            var final = CargarPrestaciones(model.Id).First();

            return Json(new { success = true, data = final, message = Valores.Creacion });
        }

        [HttpGet]
        public IActionResult EditPrestacion(int id)
        {
            var result = prestacionService.GetById(id);

            ViewBag.IdCategoriaPrestacion = new SelectList(categoriaPrestacionService.GetList(c => !c.Eliminado), "Id", "Nombre", result.IdCategoriaPrestacion);

            return PartialView("_ModalPrestacion", result);
        }

        [HttpPost]
        public IActionResult EditPrestacion(Prestacion model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = Valores.Incorrectos });
            }

            var prestacion = prestacionService.GetById(model.Id);

            prestacion.Nombre = model.Nombre;
            prestacion.Codigo = model.Codigo;
            prestacion.IdCategoriaPrestacion = model.IdCategoriaPrestacion;
            prestacion.Observaciones = model.Observaciones;
            

            prestacionService.Edit(prestacion);

            var final = CargarPrestaciones(model.Id).First();

            return Json(new { success = true, data = final, message = Valores.Edicion });
        }
                

        public IActionResult EliminarPrestacion(int id)
        {
            var model = prestacionService.GetById(id);

            model.Eliminado = true;

            prestacionService.Edit(model);

            return Json(new { success = true, message = Enum.Valores.Eliminacion });
        }


        [HttpGet]
        public IActionResult EditInlinePrecios(int id)
        {
            var result = precioService.GetList(c => c.IdPrestacion == id, c => c.ObraSocial, c => c.Prestacion)
                .Select(c => new EditPreciosVM
                {
                    Id = c.Id,
                    Prestacion = c.Prestacion.Nombre,
                    ObraSocial = c.ObraSocial.Nombre,
                    PrecioPesos = c.PrecioPesos,
                    CoseguroPesos = c.CoseguroPesos,                    
                });

            return PartialView("_ModalPreciosInline", result);
        }

        
        public IActionResult EditInlinePreciosPOST(int id, string valor, string campo)
        {
            var model = precioService.GetById(id);

            if (model == null)
            {
                return Json(new { Resultado = false, Mensaje = "Precios no encontrados" });
            }

            switch (campo)
            {
                case "precioPesos":
                    model.PrecioPesos = double.Parse(valor);
                    precioService.Edit(model);
                    return Json(new
                    {
                        Resultado = true,
                        Mensaje = "Éxito!",
                    });
                case "coseguroPesos":
                    model.CoseguroPesos = double.Parse(valor);
                    precioService.Edit(model);
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
        #endregion

        #region CATEGORIAS
        [HttpGet]
        public IActionResult CategoriasPrestacion()
        {
            ViewData["Title"] = "Categorias de Prestacion";

            return View();
        }

        [HttpGet]
        public IActionResult CargarTablaCategoriasPrestacion()
        {
            var final = CargarCategoriasPrestacion(null);

            return Json(new { success = true, data = final });
        }

        private IEnumerable<object> CargarCategoriasPrestacion(int? id)
        {
            return categoriaPrestacionService.GetList(c => (id == null ? !c.Eliminado : c.Id == id))
                .Select(c => new
                {
                    id = c.Id,
                    nombre = c.Nombre,
                    observaciones = c.Observaciones

                })
                .OrderBy(c => c.nombre);
        }

        [HttpGet]
        public IActionResult CreateCategoriaPrestacion(int id)
        {

            ViewBag.IdInsumo = new SelectList(insumoService.GetList(c => !c.Eliminado), "Id", "Nombre");

            return PartialView("_ModalCategoriaPrestacion", new CategoriaPrestacion
            {

            });
        }

        [HttpPost]
        public IActionResult CreateCategoriaPrestacion(CategoriaPrestacion model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = Valores.Incorrectos });
            }

            model.Eliminado = false;

            foreach (var item in model.Items)
            {
                model.Detalles.Add(new InsumoxCategoria
                {
                    IdInsumo = item.IdInsumo,
                    Cantidad = item.Cantidad,
                });
            }

            categoriaPrestacionService.Add(model);

            var final = CargarCategoriasPrestacion(model.Id).First();

            return Json(new { success = true, data = final, message = Valores.Creacion });
        }

        [HttpGet]
        public IActionResult EditCategoriaPrestacion(int id)
        {
            ViewBag.IdInsumo = new SelectList(insumoService.GetList(c => !c.Eliminado), "Id", "Nombre");

            var result = categoriaPrestacionService.GetById(id);

            var items = insumoxCategoriaService.GetList(c => c.IdCategoriaPrestacion == id).Select(c => new
            {
                Nombre = c.IdInsumo + "+" + c.Cantidad
            });

            //result.ItemsLoad = string.Join(';', items.Select(c => c.Nombre));

            result.ItemsLoad = JsonConvert.SerializeObject(insumoxCategoriaService.GetList(c => c.IdCategoriaPrestacion == id).Select(c => new
            {
                IdInsumo = c.IdInsumo,
                Cantidad = c.Cantidad
            }));

            return PartialView("_ModalCategoriaPrestacion", result);
        }

        [HttpPost]
        public IActionResult EditCategoriaPrestacion(CategoriaPrestacion model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = Valores.Incorrectos });
            }

            var categoria = categoriaPrestacionService.GetById(model.Id);

            var detalles = insumoxCategoriaService.GetList(c => c.IdCategoriaPrestacion == model.Id);

            insumoxCategoriaService.DeleteRange(detalles.ToArray());

            foreach (var item in model.Items)
            {
                categoria.Detalles.Add(new InsumoxCategoria
                {
                    IdInsumo = item.IdInsumo,
                    Cantidad = item.Cantidad,
                });
            }

            categoria.Nombre = model.Nombre;
            categoria.Observaciones = model.Observaciones;
            categoria.Eliminado = false;

            categoriaPrestacionService.Edit(categoria);

            var final = CargarCategoriasPrestacion(model.Id).First();

            return Json(new { success = true, data = final, message = Valores.Edicion });
        }

        public IActionResult EliminarCategoriaPrestacion(int id)
        {
            var model = categoriaPrestacionService.GetById(id);

            model.Eliminado = true;

            categoriaPrestacionService.Edit(model);

            return Json(new { success = true, message = Enum.Valores.Eliminacion });
        }

        #endregion
    }
}