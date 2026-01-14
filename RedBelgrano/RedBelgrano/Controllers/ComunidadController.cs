using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        //[HttpGet]
        //public async Task<IActionResult> Index()
        //{
        //    var publicaciones = await db.Publicaciones
        //        .Where(p => p.Habilitado)
        //        .Include(p => p.CategoriaPublicacion)
        //        .Include(p => p.Usuario)
        //        .OrderByDescending(p => p.FechaCreacion)
        //        .Select(p => new PublicacionListadoViewModel
        //        {
        //            Id = p.PublicacionId,
        //            Titulo = p.Titulo,
        //            Contenido = p.Contenido,
        //            Categoria = p.CategoriaPublicacion.Nombre,
        //            Autor = p.Usuario.nombre,
        //            FechaCreacion = p.FechaCreacion
        //        })
        //        .ToListAsync();

        //    var model = new ComunidadIndexViewModel
        //    {
        //        Publicaciones = publicaciones,
        //        NuevaPublicacion = new CrearPublicacionViewModel()
        //    };

        //    ViewBag.Categorias = await db.CategoriaPublicacion
        //        .OrderBy(c => c.Nombre)
        //        .ToListAsync();

        //    return View(model);
        //}

        public async Task<IActionResult> Index()
        {
            ViewBag.Categorias = await ObtenerCategorias();


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CrearPublicacionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categorias = await ObtenerCategorias();

                return View(model);
            }

            var publicacion = new Publicacion
            {
                Titulo = model.Titulo,
                Contenido = model.Contenido,
                CategoriaPublicacionId = model.CategoriaPublicacionId,
                FechaCreacion = DateTime.UtcNow,
                Habilitado = true,
                UsuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))
            };

            db.Publicaciones.Add(publicacion);
            await db.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        //[HttpPost]
        //public async Task<IActionResult> Crear(ComunidadIndexViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        model.Publicaciones = await ObtenerPublicaciones();
        //        ViewBag.Categorias = await db.CategoriaPublicacion
        //            .ToListAsync();

        //        return View("Index", model);
        //    }

        //    var publicacion = new Publicacion
        //    {
        //        Titulo = model.NuevaPublicacion.Titulo,
        //        Contenido = model.NuevaPublicacion.Contenido,
        //        CategoriaPublicacionId = model.NuevaPublicacion.CategoriaPublicacionId,
        //        FechaCreacion = DateTime.UtcNow,
        //        Habilitado = true,
        //        UsuarioId = int.Parse( User.FindFirstValue(ClaimTypes.NameIdentifier) )
        //    };

        //    db.Publicaciones.Add(publicacion);
        //    await db.SaveChangesAsync();

        //    return RedirectToAction("Index");
        //}

        private async Task<SelectList> ObtenerCategorias()
        {
            var categorias = await db.CategoriaPublicacion.ToListAsync();
            return new SelectList(categorias, "Id", "Nombre");
        }

    }
}
