using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RedBelgrano.Context;
using RedBelgrano.DataViewModel;
using RedBelgrano.Migrations;
using RedBelgrano.Models;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace RedBelgrano.Controllers
{
    //[Authorize]
    public class FinanzasController : Controller
    {
        private AppDBContext db;
        public FinanzasController(AppDBContext dBContext)
        {
            db = dBContext;
        }

        //VISTA DE INICIO
        public async Task<IActionResult> Inicio()
        {
            InicioTransaccionesVM vm = new InicioTransaccionesVM()
            {
                Reserva =  ObtenerReservasTotales(),
                BalanceMes = await ObtenerBalanceMesActual(),
                IngresosMes = await ObtenerIngresosMesActual(),
                GastosMes = await ObtenerGastosMesActual(),
                UltimaTransaccion = await ObtenerUltima(),
            };

            return View(vm);
        }

        //VISTA DE LISTADO
        public async Task<IActionResult> Transacciones()
        {
            List<Transaccion> transacciones = await db.Transacciones
                                    .Include(t => t.tipoTransaccion)
                                    .Include(t => t.administrador)
                                    .Include(t => t.categoria)
                                    .OrderByDescending(t => t.fecha)
                                    .ToListAsync();

            return View(transacciones);
        }

        //VISTA DE NUEVA TRANSACCION (FORMULARIO)
        public async Task<IActionResult> Nueva()
        {
            //Datos para los select del formulario
            ViewBag.Tipos = await ObtenerTipos();
            ViewBag.Categorias = await ObtenerCategorias();

            return View();
        }

        //Añadir
        [HttpPost]
        public async Task<IActionResult> AñadirTransaccion(TransaccionesVM nueva_transaccion)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.Tipos = await ObtenerTipos();
                ViewBag.Categorias = await ObtenerCategorias();

                return RedirectToAction("Nueva");
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

            return RedirectToAction("Transacciones");
        }

        //VISTA DETALLE DE TRANSACCION
        public async Task<IActionResult> Detalle(int id)
        {
            Transaccion? transaccion = await db.Transacciones
                .Include(t => t.tipoTransaccion)
                .Include(t => t.administrador)
                .Include(t => t.categoria)
                .FirstOrDefaultAsync(r => r.transaccionId == id);

            if (transaccion == null)
            {
                return NotFound();
            }

            return View(transaccion);
        }

        //VISTA DE GRAFICOS
        public IActionResult Graficos()
        {
            return View();
        }


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

        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <returns></returns>

        //INFORMACION PARA GRAFICOs
        public async Task<IActionResult> ObtenerIngresosPorTipo() //funciona bien
        {
            var transaccionesPorTipo = await db.Transacciones
                .Where(t => t.fecha.Year == DateTime.Now.Year && t.tipoTransaccion.nombre.Equals("ingreso") )
                .GroupBy(t => t.categoria.nombre)
                .Select(g => new
                {
                    Tipo = g.Key,
                    Total = g.Sum(t => t.monto)
                })
                .OrderByDescending(x => x.Total)
                .ToListAsync();

            foreach (var item in transaccionesPorTipo)
            {
                Console.WriteLine(item);
            }

            return Ok(transaccionesPorTipo);
        }
        public async Task<IActionResult> ObtenerGastosPorTipo() //funciona bien
        {
            var transaccionesPorTipo = await db.Transacciones
                .Where(t => t.fecha.Year == DateTime.Now.Year && t.tipoTransaccion.nombre.Equals("gasto"))
                .GroupBy(t => t.categoria.nombre)
                .Select(g => new
                {
                    Tipo = g.Key,
                    Total = g.Sum(t => t.monto)
                })
                .OrderByDescending(x => x.Total)
                .ToListAsync();

            foreach (var item in transaccionesPorTipo)
            {
                Console.WriteLine(item);
            }

            return Ok(transaccionesPorTipo);
        }

        public async Task<IActionResult> ObtenerBalanceDeMeses() //tambien funciona bien
        {
            var netoPorMes = await db.Transacciones
                .Where(t => t.fecha.Year == DateTime.Now.Year)
                .Select(t => new  //Convierte cada transacción en un objeto simplificado con: El año y mes de la transacción y el monto , negativo si es gasto
                {
                    Año = t.fecha.Year,
                    Mes = t.fecha.Month,
                    Monto = t.tipoTransaccion.nombre == "Gasto" ? -t.monto : t.monto
                })
                /* Resultado Parcial
                 * Año Mes Monto
                 2025    1   1000
                 2025    1 - 200
                 2025    2   500
                 2025    2 - 800*/

                .GroupBy(t => new { t.Año, t.Mes })/*  Grupo 1(Año = 2025, Mes = 1): [1000, -200],
                                                       Grupo 2(Año = 2025, Mes = 2): [500, -800]   */
                .Select(g => new
                {
                    Año = g.Key.Año,
                    Mes = g.Key.Mes,
                    TotalNeto = g.Sum(t => t.Monto) //Se suman los montos de los grupos
                })
                .OrderBy(x => x.Año).ThenBy(x => x.Mes)
                .ToListAsync();

            return Ok(netoPorMes);
        }

        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////
        /// </summary>

        // Datos de Mes Actual
        public async Task<RegistroMensualVM?> ObtenerBalanceMesActual() //otro que funciona bien
        {
            RegistroMensualVM? balance = await db.Transacciones
                .Select(t => new  //Convierte cada transacción en un objeto simplificado con: El año y mes de la transacción y el monto , negativo si es gasto
                {
                    Año = t.fecha.Year,
                    Mes = t.fecha.Month,
                    Monto = t.tipoTransaccion.nombre == "Gasto" ? -t.monto : t.monto
                })
                .GroupBy(t => new { t.Año, t.Mes })
                .Select(g => new RegistroMensualVM
                {
                    Año = g.Key.Año,
                    Mes = g.Key.Mes,
                    Total = g.Sum(t => t.Monto)
                })
                .Where(g => g.Mes == DateTime.Now.Month && g.Año == DateTime.Now.Year) //Se seleccina el mes actual
                .FirstOrDefaultAsync();

            return balance;
        }

        public async Task<RegistroMensualVM> ObtenerIngresosMesActual() //funca
        {
            RegistroMensualVM? ingresos = await db.Transacciones
                .Select(t => new 
                { 
                    Año = t.fecha.Year,
                    Mes = t.fecha.Month,
                    Monto = t.tipoTransaccion.nombre == "Ingreso" ? t.monto : 0
                })
                .GroupBy(t => new { t.Año, t.Mes })
                .Select(g => new RegistroMensualVM
                {
                    Año = g.Key.Año,
                    Mes = g.Key.Mes,
                    Total = g.Sum(t => t.Monto)
                })
                .Where(g => g.Mes == DateTime.Now.Month && g.Año == DateTime.Now.Year) //Se seleccina el mes actual
                .FirstOrDefaultAsync();

            return ingresos;
        }

        public async Task<RegistroMensualVM> ObtenerGastosMesActual() //funca
        {
            RegistroMensualVM? gastos = await db.Transacciones
                .Select(t => new
                {
                    Año = t.fecha.Year,
                    Mes = t.fecha.Month,
                    Monto = t.tipoTransaccion.nombre == "Gasto" ? t.monto : 0
                })
                .GroupBy(t => new { t.Año, t.Mes })
                .Select(g => new RegistroMensualVM
                {
                    Año = g.Key.Año,
                    Mes = g.Key.Mes,
                    Total = g.Sum(t => t.Monto)
                })
                .Where(g => g.Mes == DateTime.Now.Month && g.Año == DateTime.Now.Year) //Se seleccina el mes actual
                .FirstOrDefaultAsync();

            return gastos;
        }

        public async Task<Transaccion?> ObtenerUltima()
        {
            Transaccion? ultima = await db.Transacciones
                .OrderByDescending(t => t.fecha)
                .FirstOrDefaultAsync();

            return ultima;
        }

    }
}
