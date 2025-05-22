using Microsoft.AspNetCore.Mvc;

namespace RedBelgrano.Controllers
{
    public class FinanzasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
