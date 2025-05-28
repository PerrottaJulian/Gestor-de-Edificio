using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RedBelgrano.Context;
using RedBelgrano.DataViewModel;
using RedBelgrano.Models;

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

        public /*async Task<*/IActionResult/*>*/ Index()
        {
            //await ObtenerEstados();
            return View();
        }

        public async Task<IActionResult> Nuevo()
        {
            try
            {
                ViewBag.Tipos = await ObtenerTipos();
                ViewBag.Estados = await ObtenerEstados();
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
            }

            return View();
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


        public async Task<SelectList> ObtenerTipos()
        {
            var tipos = await db.TipoResidente.ToListAsync();
            return new SelectList(tipos, "tipoRId", "tipo");

        }

        public async Task<SelectList> ObtenerEstados()
        {
            var estados = await db.EstadoResidente.ToListAsync();
            return new SelectList(estados, "estadoId", "estado");

        }



    }
    
}
