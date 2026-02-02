using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RedBelgrano.Context;
using RedBelgrano.DataViewModel;
using RedBelgrano.Models;
using System.Security.Claims;

[Authorize]
public class ComunidadController : Controller
{
    private readonly AppDBContext db;
    private const int PAGE_SIZE = 10;

    public ComunidadController(AppDBContext context)
    {
        db = context;
    }

    // ===================== GET =====================
    public async Task<IActionResult> Index(int? categoriaId, int page = 1, string view = "form")
    {
        ViewBag.Categorias = await ObtenerCategorias();
        ViewBag.CategoriaSeleccionada = categoriaId;

        var query = db.Publicaciones
            .Include(p => p.CategoriaPublicacion)
            .Include(p => p.Usuario)
            .Where(p => p.Habilitado)
            .AsQueryable();

        // Filtro por categoría
        if (categoriaId.HasValue && categoriaId.Value > 0)
        {
            query = query.Where(p => p.CategoriaPublicacionId == categoriaId.Value);
        }

        var totalPublicaciones = await query.CountAsync();

        var publicaciones = await query
            .OrderByDescending(p => p.FechaCreacion)
            .Skip((page - 1) * PAGE_SIZE)
            .Take(PAGE_SIZE)
            .ToListAsync();

        ViewBag.Publicaciones = publicaciones;
        ViewBag.PaginaActual = page;
        ViewBag.TotalPaginas = (int)Math.Ceiling(totalPublicaciones / (double)PAGE_SIZE);
        ViewBag.VistaActiva = view;

        return View();
    }

    // ===================== POST =====================
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(CrearPublicacionViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categorias = await ObtenerCategorias();
            ViewBag.Publicaciones = new List<Publicacion>();
            ViewBag.PaginaActual = 1;
            ViewBag.TotalPaginas = 1;

            return View(model);
        }

        var publicacion = new Publicacion
        {
            Titulo = model.Titulo,
            Contenido = model.Contenido,
            CategoriaPublicacionId = model.CategoriaPublicacionId,
            FechaCreacion = DateTime.UtcNow,
            Habilitado = true,
            UsuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)
        };

        db.Publicaciones.Add(publicacion);
        await db.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    // ===================== HELPERS =====================
    private async Task<SelectList> ObtenerCategorias()
    {
        var categorias = await db.CategoriaPublicacion
            .OrderBy(c => c.Nombre)
            .ToListAsync();

        return new SelectList(categorias, "Id", "Nombre");
    }
}

