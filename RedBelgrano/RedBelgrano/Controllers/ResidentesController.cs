using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RedBelgrano.Context;

namespace RedBelgrano.Controllers
{
    [Authorize]
    public class ResidentesController : Controller
    {
        public AppDBContext db;

        public ResidentesController(AppDBContext _context)
        {
            db = _context;
        }

        public async Task<IActionResult> Index()
        {
            //await ObtenerEstados();
            return View();
        }

        public async Task<IActionResult> Nuevo()
        {
            return View();
        }

        public async Task ObtenerTipos()
        {
            var tipos = await db.TipoResidente.ToListAsync();
            /*return new SelectList(tipos, "Id", "Tipo");*/
            Console.WriteLine($"Cantidad de tipos: {tipos.Count}");
            foreach (var tipo in tipos)
            {
                Console.WriteLine($"{tipo.tipoRId}: {tipo.tipo}");
            }
        }

        public async Task ObtenerEstados()
        {
            var tipos = await db.EstadoResidente.ToListAsync();
            /*return new SelectList(tipos, "Id", "Tipo");*/
            Console.WriteLine($"Cantidad de tipos: {tipos.Count}");
            foreach (var tipo in tipos)
            {
                Console.WriteLine($"{tipo.estadoId}: {tipo.estado}");
            }
        }

    }
    
}
