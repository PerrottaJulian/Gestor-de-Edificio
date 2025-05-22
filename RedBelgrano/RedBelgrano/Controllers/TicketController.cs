using Microsoft.AspNetCore.Mvc;

namespace RedBelgrano.Controllers
{
    public class TicketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
