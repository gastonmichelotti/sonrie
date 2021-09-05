using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using netCoreNew.Business;
using netCoreNew.Enum;
using netCoreNew.Models;
using netCoreNew.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace netCoreNew.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IUsuarioService usuarioService;
        private readonly IOptions<Logging> logging;
        private readonly IConfiguration iconfiguration;
        private readonly IRolService rolService;

        public HomeController(IUsuarioService usuarioService,
            IOptions<Logging> logging,
            IConfiguration iconfiguration,
            IRolService rolService)
        {
            this.usuarioService = usuarioService;
            this.logging = logging;
            this.iconfiguration = iconfiguration;
            this.rolService = rolService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if(User.Identity.IsAuthenticated)
            {
                var usuario = usuarioService.GetByEmail(User.Identity.Name);

                var rol = rolService.GetById(usuario.IdRol);

                return Redirect(rol.Redirect);
            }

            ViewBag.Title = Valores.Slogan;

            return View();
        }

        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();

            return RedirectToAction("Index");
        }

        public void CrearSesion(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Rol.Nombre)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.SignInAsync(new ClaimsPrincipal(identity));
        }

        [Route("error")]
        public IActionResult Error(ErrorVM model)
        {
            var builder = new StringBuilder();

            var uploads = "./wwwroot/Templates/Error";
            var filePath = Path.Combine(uploads, "Error.html");

            using (var reader = System.IO.File.OpenText(filePath))
            {
                builder.Append(reader.ReadToEnd());
            }

            builder.Replace("{{titulo}}", model.Titulo);
            builder.Replace("{{subtitulo}}", model.Subtitulo);
            builder.Replace("{{descripcion}}", model.Descripcion);
            builder.Replace("{{bg}}", "bg5");

            return new ContentResult()
            {
                Content = builder.ToString(),
                ContentType = "text/html",
            };
        }

        [Route("errorpartial")]
        public IActionResult ErrorPartial(ErrorVM model)
        {
            return PartialView("_Error", model);
        }

        [HttpPost]
        public JsonResult Login(LoginVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception(Valores.Incorrectos);
                }

                var usuario = usuarioService.GetSingle(c => c.Email == model.Email && c.Password == model.Password, c => c.Rol);

                if (usuario == null)
                {
                    throw new Exception(Valores.Incorrectos);
                }

                if (!usuario.Activo)
                {
                    throw new Exception("Usuario desactivado");
                }

                CrearSesion(usuario);

                return Json(new { success = true, message = "Bienvenido! Serás redireccionado.", redirect = usuario.Rol.Redirect });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }
    }
}
