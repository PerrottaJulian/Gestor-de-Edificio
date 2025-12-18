using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedBelgrano.Context;
using RedBelgrano.DataViewModel;
using RedBelgrano.Migrations;
using RedBelgrano.Models;
using System.Security.Claims;

namespace RedBelgrano.Controllers
{
    public class PerfilController : Controller
    {

        private AppDBContext db;
        public PerfilController(AppDBContext dBContext)
        {
            db = dBContext;
        }

        // GET: Perfil
        public async Task<IActionResult> Index()
        {
            int usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var usuario = await db.Usuarios
                                        .FirstOrDefaultAsync(u => u.usuarioId == usuarioId);

            if (usuario == null)
                return RedirectToAction("IniciarSesion", "Auth");

            var perfilVM = new PerfilVM
            {
                UsuarioId = usuario.usuarioId,
                TipoUsuario = usuario.tipo,
                Nombre = usuario.nombre,
                Dni = usuario.dni,
                Email = usuario.email
            };

            if (usuario.tipo == "Residente")
            {
                var residente = await db.Residentes
                    .Include(r => r.tipoResidente)
                    .Include(r => r.estadoResidente)
                    .FirstOrDefaultAsync(r => r.dni == usuario.dni);

                if (residente != null)
                {
                    perfilVM.Residente = new ResidentePerfilVM
                    {
                        Nombre = residente.nombre,
                        Apellido = residente.apellido,
                        Dni = residente.dni,
                        Email = residente.email,
                        Telefono = residente.telefono,
                        Piso = residente.piso,
                        Departamento = residente.departamento,
                        TipoResidente = residente.tipoResidente?.tipo,
                        Estado = residente.estadoResidente?.estado
                    };
                }
            }

            return View(perfilVM);
        }


        public IActionResult CambiarClave()
        {
            return View(new CambiarClaveVM());
        }

        // POST: Perfil/CambiarClave
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CambiarClave(CambiarClaveVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            int usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var usuario = await db.Usuarios.FirstOrDefaultAsync(u => u.usuarioId == usuarioId);

            if (usuario == null)
            {
                ModelState.AddModelError("", "Usuario no encontrado.");
                return View(model);
            }

            // Validación de contraseña actual
            if (!usuario.VerificarClave(model.ClaveActual))
            {
                ModelState.AddModelError("ClaveActual", "La contraseña actual no es correcta.");
                return View(model);
            }

            // Validación de coincidencia
            if (model.NuevaClave != model.ConfirmarClave)
            {
                ModelState.AddModelError("ConfirmarClave", "Las contraseñas no coinciden.");
                return View(model);
            }

            // Cambio de contraseña
            usuario.clave = model.NuevaClave;

            db.Usuarios.Update(usuario);
            await db.SaveChangesAsync();

            TempData["Mensaje"] = "La contraseña se cambió correctamente.";
            return RedirectToAction("Index");
        }


    }
}
