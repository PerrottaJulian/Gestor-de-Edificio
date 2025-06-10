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
        public  IActionResult IniciarSesion()
        {
            ViewBag.seeNavbar = false;

            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Residentes");

            }else
            {
                //await Despertador(); //Espera a que la base de datos se despierte, para que luego no haya problemas ni errores de timeout
                //usar una flag estatica para que solo se haga una vez

                return View();

            }

        }

        [HttpPost]
        public async Task<IActionResult> IniciarSesion(UsuarioLoginVM _usuario)
        
        {
            Usuario? usuario = null;
            Usuario? usuario_encontrado = null;
            try
            {
                usuario_encontrado = await db.Usuarios.Where(u =>
                             u.dni == _usuario.dni &&
                             //u.clave.Equals(_usuario.clave) &&
                             u.tipo.Equals(_usuario.tipo))
                            .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = $"Ocurrio un Error Inesperado\n{ex.Message}";
                return View();
            }

            if (usuario_encontrado == null)
            {
                ViewData["Mensaje"] = "No se encontro Usuario";
                return View();
            }

            //Verifica si la contraseña (encriptada) es correcta 
            if(!usuario_encontrado.VerificarClave(_usuario.clave))
            {
                ViewData["Mensaje"] = "Contraseña Incorrecta";
                return View();
            }

            usuario = usuario_encontrado;

            //Claims, para guardar la data de el usuario entrante

            List<Claim> claims = new List<Claim>() 
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.usuarioId.ToString()),
                new Claim(ClaimTypes.Name, usuario.nombre),
                new Claim(ClaimTypes.Role, usuario.tipo),
                new Claim(ClaimTypes.Email, usuario.email),

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




        //Despertar base de datos
        /*public async Task Despertador()
        {
            try
            {
                await db.Database.ExecuteSqlRawAsync("SELECT 1");
                await Console.Out.WriteLineAsync("SE DESPERTO");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al despertar la base: " + ex.Message);
            }
        }*/

    }
}
