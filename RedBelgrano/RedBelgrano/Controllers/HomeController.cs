using Microsoft.AspNetCore.Mvc;
using RedBelgrano.Context;
using RedBelgrano.Models;
using System.Diagnostics;

namespace RedBelgrano.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private AppDBContext db;

        public HomeController(ILogger<HomeController> logger, AppDBContext dBContext)
        {
            _logger = logger;
            db = dBContext;
        }

        public IActionResult Index()
        {
            ViewBag.seeNavbar = false;
            //PrimerUsuario();
            return View();
        }


        private void PrimerUsuario()
        {
            try
            {
                if (!db.Usuarios.Any())
                {
                    Usuario usuario = new Usuario();
                    usuario.tipo = "Administrador";
                    usuario.dni = 44563116;
                    usuario.nombre = "Julian Perrotta";
                    usuario.email = "perrotta.julian12@gmail.com";
                    usuario.clave = "contraseña123";

                    db.Usuarios.Add(usuario);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                TempData["MensajeError"] = "Ocurrio una excepcion: "+ ex;
            }
            
        }

    }
}
