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
            TransaccionesVM vm = await InicializarVM();

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AñadirTransaccion(TransaccionesVM nueva_transaccion)
        {

            if(!ModelState.IsValid)
            {
                ViewBag.tipos = await ObtenerTipos();
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
            };

            await db.Transacciones.AddAsync(transaccion);
            await db.SaveChangesAsync();



            return RedirectToAction("Index");
        }

        //OBTENER TRANSACCIONES



        //Obtener datos
        private async Task<SelectList> ObtenerTipos()
        {
            var tipos = await db.TipoTransaccion.ToListAsync();
            return new SelectList(tipos, "tipoTId", "nombre");
        }

        //Iniciarlizar lista de transacciones
        private async Task<TransaccionesVM> InicializarVM()
        {
            return new TransaccionesVM()
            {
                transaccions = await db.Transacciones
                                    .Include(t => t.tipoTransaccion)
                                    .Include(t => t.administrador)
                                    .ToListAsync()
            };
        }


    }
}
