using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using netCoreNew.Handlers;
using System;

namespace netCoreNew.Controllers
{
    [HandleException]
    public abstract class BaseController : Controller
    {
        public static DateTime CurrentDate => DateTime.UtcNow.AddHours(-3);

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

        }
    }
}