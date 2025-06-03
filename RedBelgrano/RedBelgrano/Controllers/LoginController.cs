using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedBelgrano.Context;
using RedBelgrano.Models;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using RedBelgrano.DataViewModel;

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
        public IActionResult IniciarSesion()
        {
            ViewBag.seeNavbar = false;

            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");

            }else
            {
                return View();

            }

        }

        [HttpPost]
        public async Task<IActionResult> IniciarSesion(Usuario _usuario)
        {
            Usuario? usuario = null;
            try
            {
                usuario = await db.Usuarios.Where(u =>
                             u.dni == _usuario.dni &&
                             u.clave.Equals(_usuario.clave) &&
                             u.tipo.Equals(_usuario.tipo))
                            .FirstOrDefaultAsync();

            }catch (Exception ex)
            {
                ViewData["Mensaje"] = $"Ocurrio un Error Inesperado\n{ex.Message}";
                return View();
            }

            if (usuario == null)
            {
                ViewData["Mensaje"] = "No se encontro Usuario";
                return View();
            }

            List<Claim> claims = new List<Claim>() //En caso de necesitar mas data del usuario, poner mas claims
            {
                new Claim(ClaimTypes.Name, usuario.nombre)
                //Para identificar el rol, usar ClaimTypes.Role, usuario.Tipo
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties
                );

            /*para futuro cerrar sesion:
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); //poner en el HomeController*/

            Console.WriteLine("Ingreso el Usuario: " + usuario.nombre );
            return RedirectToAction("Index", "Residentes");

            
        }

    }
}
