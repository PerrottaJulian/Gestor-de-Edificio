using Microsoft.AspNetCore.Mvc;

namespace RedBelgrano.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

    }
}
