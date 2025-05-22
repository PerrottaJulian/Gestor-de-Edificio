using Microsoft.AspNetCore.Mvc;

namespace RedBelgrano.Controllers
{
    public class ResidentesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
