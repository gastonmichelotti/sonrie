using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using netCoreNew.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace netCoreNew.Handlers
{
    public class HandleExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception != null)
            {
                var stack = context.Exception.StackTrace;
                var msg = context.Exception.Message;
                if (context.ModelState != null && context.ModelState.Any(c => c.Value.Errors.Any()))
                {
                    var estado = context.ModelState.IsValid;
                    var errores = context.ModelState.Values.SelectMany(c => c.Errors);
                }

                if (context.Exception is UnauthorizedAccessException)
                {
                    msg = "No tienes acceso a esa pagina.";
                }
                else if (context.Exception is NullReferenceException)
                {

                }
                else if (context.Exception is TimeoutException)
                {
                    msg = "Tiempo de espera excedido.";
                }

                var error = new ErrorVM
                {
                    Titulo = "Ups!",
                    Descripcion = "",
                    Subtitulo = msg == null ? "Surgió un error inesperado!" : msg
                };

                var isAjax = context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
                var isGet = context.HttpContext.Request.Method == "GET";

                if (isAjax)
                {
                    if(isGet)
                    {
                        context.Result = new RedirectToActionResult("ErrorPartial", "Home", error);
                    }
                    else
                    {
                        context.Result = new JsonResult(new { success = false, message = error.Subtitulo });
                    }
                }
                else
                {
                    context.Result = new RedirectToActionResult("Error", "Home", error);
                }

                context.ExceptionHandled = true;
            }
            else
            {
                base.OnException(context);
            }
        }
    }
}
