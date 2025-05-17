using Microsoft.AspNetCore.Mvc;
using RedBelgrano.Models;
using System.Timers;

namespace RedBelgrano.Controllers
{
    public class LoginController : Controller
    {

        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.seeNavbar = false;
            return View();
        }
        [HttpPost]
        public IActionResult Login(UsuarioAdmin admin)
        {

            return View();
        }

    }
}
