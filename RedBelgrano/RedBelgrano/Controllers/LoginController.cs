using Microsoft.AspNetCore.Mvc;
using RedBelgrano.Models;
using System.Timers;

namespace RedBelgrano.Controllers
{
    public class LoginController : Controller
    {

        UsuarioAdmin helper_usuario = new UsuarioAdmin();

        public LoginController()
        {
            helper_usuario.dni = 44563116;
            helper_usuario.nombre = "Julian Perrotta";
            helper_usuario.email = "perrotta.julian12@gmail.com";
            helper_usuario.clave = "admin";
        }

        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.seeNavbar = false;
            return View();
        }
        [HttpPost]
        public IActionResult Login(UsuarioAdmin admin)
        {

            if (admin == null )
            {
                return Login();
            }

            if (admin.dni != helper_usuario.dni)
            {
                Console.WriteLine("No existe ese Usuario");
                return Login();
            }
            else if (!admin.clave.Equals(helper_usuario.clave))
            {
                Console.WriteLine("La contraseña no coincide");
                return Login();
            }

            Console.WriteLine("Se logueo correctamente");
            return RedirectToAction("Index", "Home");
        }

    }
}
