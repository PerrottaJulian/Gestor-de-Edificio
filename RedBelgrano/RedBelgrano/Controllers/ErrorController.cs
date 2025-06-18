using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace RedBelgrano.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error")]
        public IActionResult Index()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerPathFeature>()?.Error;

            if (exception is InvalidOperationException && exception.Message.Contains("The view"))
            {
                // Error de vista no encontrada
                return View("PageNotFound");
            }

            // Otro error
            return View(exception);
        }
        public IActionResult PageNotFound()
        {
            
            return View();
        }
        public IActionResult AccesoDenegado()
        {
            var referer = Request.Headers["Referer"].ToString();

            if (!string.IsNullOrEmpty(referer))
            {
                return Redirect(referer); // Volver a la vista anterior
            }

            return RedirectToAction("Index", "Home"); // Por si no hay referer
        }

    }
}
