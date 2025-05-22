using Microsoft.AspNetCore.Mvc;

namespace RedBelgrano.Controllers
{
    public class ComunidadController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
