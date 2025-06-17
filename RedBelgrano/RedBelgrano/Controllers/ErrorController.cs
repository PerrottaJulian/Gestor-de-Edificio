using Microsoft.AspNetCore.Mvc;

namespace RedBelgrano.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index(Exception ex)
        {
            return View(ex);
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
