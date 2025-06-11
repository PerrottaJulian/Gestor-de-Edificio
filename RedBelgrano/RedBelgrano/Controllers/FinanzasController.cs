  using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RedBelgrano.Context;
using RedBelgrano.DataViewModel;
using RedBelgrano.Models;
using System.Security.Claims;

namespace RedBelgrano.Controllers
{
    public class FinanzasController : Controller
    {
        private AppDBContext db;
        public FinanzasController(AppDBContext dBContext) 
        {
            db = dBContext; 
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Tipos = await ObtenerTipos();
            ViewBag.Categorias = await ObtenerCategorias();
            ViewBag.Reserva = ObtenerReservasTotales();

            Console.Clear();


            TransaccionesVM vm = await InicializarVM();

            return View(vm);
        }

        //Añadir
        [HttpPost]
        public async Task<IActionResult> AñadirTransaccion(TransaccionesVM nueva_transaccion)
        {

            if(!ModelState.IsValid)
            {
                ViewBag.Tipos = await ObtenerTipos();
                ViewBag.Categorias = await ObtenerCategorias();
                ViewBag.Reserva = ObtenerReservasTotales();

                return RedirectToAction("Index");
            }

            //string? UsuarioIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);

            int usuarioId;

            if (claim != null && int.TryParse(claim.Value, out int _usuarioId))
            {
                usuarioId = _usuarioId;
            }
            else
            {
                // No se pudo obtener (usuario no logueado o claim mal formado)
                return View("Error"); //cambiar por: cubrir todo el metodo en un try-catch, y aqui hacer un throw
            }

            Transaccion transaccion = new Transaccion() 
            {
                monto = nueva_transaccion.monto,
                detalle = nueva_transaccion.detalle,
                tipoId = nueva_transaccion.tipoId,
                administradorId = usuarioId,
                categoriaId = nueva_transaccion.categoriaId,
            };

            await db.Transacciones.AddAsync(transaccion);
            await db.SaveChangesAsync();



            return RedirectToAction("Index");
        }

        //OBTENER TRANSACCIONES //Inicializar lista de transacciones
        private async Task<TransaccionesVM> InicializarVM()
        {
            return new TransaccionesVM()
            {
                transacciones = await db.Transacciones
                                    .Include(t => t.tipoTransaccion)
                                    .Include(t => t.administrador)
                                    .Include(t => t.categoria)
                                    .OrderByDescending(t => t.fecha)
                                    .ToListAsync()
            };
        }

        //INFORMACION PARA GRAFICOS




        //Obtener datos varios
        private async Task<SelectList> ObtenerTipos()
        {
            var tipos = await db.TipoTransaccion.ToListAsync();
            return new SelectList(tipos, "tipoTId", "nombre");
        }

        private async Task<SelectList> ObtenerCategorias()
        {
            var categorias = await db.CategoriaTransaccion.ToListAsync();
            return new SelectList(categorias, "categoriaId", "nombre");
        }

        private double ObtenerReservasTotales()
        {
            decimal data = db.Database.SqlQueryRaw<decimal>("EXEC ObtenerReservasTotales").AsEnumerable().FirstOrDefault();

            return Convert.ToDouble(data);
          
            
        }
        
        


    }
}
