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
        public IActionResult Index()
        {
            ViewBag.tipos = ObtenerTipos();
            //ViewBag.reserva = ObtenerReserva();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AñadirTransaccion(AñadirTransaccionVM nueva_transaccion)
        {

            

            if(!ModelState.IsValid)
            {
                ViewBag.tipos = ObtenerTipos();

                return View("Index",nueva_transaccion);
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
                return View();
            }

            Transaccion transaccion = new Transaccion() 
            {
                monto = nueva_transaccion.monto,
                detalle = nueva_transaccion.detalle,
                administradorId = User.FindFirst
            };    

            return View("Index");
        }



        //Obtener datos
        private async Task<SelectList> ObtenerTipos()
        {
            var tipos = await db.TipoTransaccion.ToListAsync();
            return new SelectList(tipos, "tipoRId", "tipo");
        }


    }
}
