using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RedBelgrano.Context;
using RedBelgrano.DataViewModel;
using RedBelgrano.Models;

namespace RedBelgrano.Controllers
{
    //[Authorize]
    public class ResidentesController : Controller
    {
        public AppDBContext db;

        public ResidentesController(AppDBContext _context)
        {
            db = _context;
        }

        public async Task<IActionResult> Index()
        {
            List<Residente> residentes = [];
            try
            { 
                residentes = await db.Residentes
                    .Include(r => r.tipoResidente)
                    .Include(r => r.estadoResidente)
                    .ToListAsync();
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }

            foreach (var residente in residentes)
            {
                Console.WriteLine(residente.nombre + " " + residente.apellido + " "+ residente.dni);
                Console.WriteLine("TIPO: "+ residente.tipoResidente.tipo);
                Console.WriteLine("ESTADO: " + residente.estadoResidente.estado);

            }

            return View(residentes);
        }

        public async Task<IActionResult> Nuevo()
        {
            try
            {
                ViewBag.Tipos = await ObtenerTipos();
                ViewBag.Estados = await ObtenerEstados();
                return View();
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
                return RedirectToAction("Index", "Residentes");
            }

            
        }

        [HttpPost]
        public async Task<IActionResult> Nuevo(AñadirResidenteVM nuevo_residente)
        {
            bool dniExiste = await db.Residentes.AnyAsync(r => r.dni == nuevo_residente.dni);

            if (dniExiste) 
            {
                ModelState.AddModelError("dni", "Ya existe usuario con este DNI");

                //ViewBag.Tipos = await ObtenerTipos();
                //ViewBag.Estados = await ObtenerEstados();

                //return View(nuevo_residente);

            }

            if(!ModelState.IsValid) 
            {
                ViewBag.Tipos = await ObtenerTipos();
                ViewBag.Estados = await ObtenerEstados();

                return View(nuevo_residente);
            }

            Residente residente = new Residente()
            {
                nombre = nuevo_residente.nombre,
                apellido = nuevo_residente.apellido,
                dni = nuevo_residente.dni,
                email = nuevo_residente.email,
                telefono = nuevo_residente.telefono,
                piso = nuevo_residente.piso,
                departamento = nuevo_residente.departamento,
                tipoRId = nuevo_residente.tipoRId,
                estadoId = nuevo_residente.estadoId,
            };

            await db.Residentes.AddAsync(residente);
            await db.SaveChangesAsync();

            return RedirectToAction("Index", "Residentes");
            

        }

        //Detalle
        public async Task<IActionResult> Detalle(int id)
        {
            Residente? residente = await db.Residentes.FindAsync(id);

            if (residente == null)
            {
                return NotFound();
            }

            return View(residente);
        }

        //Dar de baja
        [HttpPost]
        public async Task<IActionResult> DarDeBaja(int id) //poner boton en la vista Detalle
        {
            Residente? residente = db.Residentes.Find(id);

            try
            {
                if(residente != null && residente.fechaBaja == null)
                {
                    await db.Database.ExecuteSqlInterpolatedAsync($"UPDATE Residente SET fechaBaja = GETDATE() WHERE residenteId = {residente.residenteId}");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            return RedirectToAction("Index", "Residentes");

        }



        //Obtener datos
        private async Task<SelectList> ObtenerTipos()
        {
            var tipos = await db.TipoResidente.ToListAsync();
            return new SelectList(tipos, "tipoRId", "tipo");

        }

        private async Task<SelectList> ObtenerEstados()
        {
            var estados = await db.EstadoResidente.ToListAsync();
            return new SelectList(estados, "estadoId", "estado");

        }



    }
    
}
