using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedBelgrano.Context;
using RedBelgrano.Models;
using System.Security.Principal;
using System.Timers;

namespace RedBelgrano.Controllers
{
    public class LoginController : Controller
    {
        private AppDBContext db;
        public LoginController(AppDBContext dBContext) 
        {
            db = dBContext;
        }

        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.seeNavbar = false;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Usuario _usuario)
        {
            Usuario? usuario = await 
                db.Usuarios.Where(u => 
                 u.dni == _usuario.dni &&
                 u.clave.Equals(_usuario.clave) && 
                 u.tipo.Equals(_usuario.tipo) )
                .FirstOrDefaultAsync();

            if (usuario == null)
            {
                ViewData["Mensaje"] = "No se encontro Usuario";
                
                return View();
            }

            Console.WriteLine("Ingreso el Usuario: " + usuario.nombre );
            return RedirectToAction("Index", "Home");

            
        }

    }
}
