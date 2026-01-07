using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedBelgrano.Context;
using RedBelgrano.DataViewModel;
using RedBelgrano.Models;
using System.Security.Claims;

namespace RedBelgrano.Controllers
{

    [Authorize]
    public class ComunidadController : Controller
    {

        private readonly AppDBContext db;

        public ComunidadController(AppDBContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            var publicaciones = await db.Publicaciones
                .Where(p => p.Habilitado)
                .Include(p => p.CategoriaPublicacion)
                .Include(p => p.Usuario)
                .OrderByDescending(p => p.FechaCreacion)
                .Select(p => new PublicacionListadoViewModel
                {
                    Id = p.PublicacionId,
                    Titulo = p.Titulo,
                    Contenido = p.Contenido,
                    Categoria = p.CategoriaPublicacion.Nombre,
                    Autor = p.Usuario.nombre,
                    FechaCreacion = p.FechaCreacion
                })
                .ToListAsync();

            var model = new ComunidadIndexViewModel
            {
                Publicaciones = publicaciones,
                NuevaPublicacion = new CrearPublicacionViewModel()
            };

            ViewBag.Categorias = await db.CategoriaPublicacion
                .OrderBy(c => c.Nombre)
                .ToListAsync();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(ComunidadIndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Publicaciones = await ObtenerPublicaciones();
                ViewBag.Categorias = await db.CategoriaPublicacion
                    .ToListAsync();

                return View("Index", model);
            }

            var publicacion = new Publicacion
            {
                Titulo = model.NuevaPublicacion.Titulo,
                Contenido = model.NuevaPublicacion.Contenido,
                CategoriaPublicacionId = model.NuevaPublicacion.CategoriaPublicacionId,
                FechaCreacion = DateTime.UtcNow,
                Habilitado = true,
                UsuarioId = int.Parse( User.FindFirstValue(ClaimTypes.NameIdentifier) )
            };

            db.Publicaciones.Add(publicacion);
            await db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private async Task<List<PublicacionListadoViewModel>> ObtenerPublicaciones()
        {
            return await db.Publicaciones
                .Where(p => p.Habilitado)
                .Include(p => p.CategoriaPublicacion)
                .Include(p => p.Usuario)
                .OrderByDescending(p => p.FechaCreacion)
                .Select(p => new PublicacionListadoViewModel
                {
                    Id = p.PublicacionId,
                    Titulo = p.Titulo,
                    Contenido = p.Contenido,
                    Categoria = p.CategoriaPublicacion.Nombre,
                    Autor = p.Usuario.nombre,
                    FechaCreacion = p.FechaCreacion
                })
                .ToListAsync();
        }



    }
}
