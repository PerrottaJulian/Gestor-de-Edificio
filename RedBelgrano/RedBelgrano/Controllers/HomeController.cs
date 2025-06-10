using Microsoft.AspNetCore.Mvc;
using RedBelgrano.Context;
using RedBelgrano.Models;
using System.Diagnostics;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            if (User.Identity!.IsAuthenticated)
            {
                ViewBag.seeNavbar = true;
            }
            else
            {
                ViewBag.seeNavbar = false;
            }
            
            //PrimerUsuario();
            return View();
        }


        private void PrimerUsuario()
        {
            try
            {
                if (!db.Usuarios.Any())
                {
                    Usuario usuario = new Usuario()
                    {
                        tipo = "Administrador",
                        dni = 44563116,
                        nombre = "Julian Perrotta",
                        email = "perrotta.julian12@gmail.com",
                        clave = "admin",
                };
                    

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
