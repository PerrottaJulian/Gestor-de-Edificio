﻿using Microsoft.AspNetCore.Authorization;
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

            return View(residentes);
        }

        //Nuevo
        [Authorize(Roles = "Administrador")]
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

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<IActionResult> Nuevo(AñadirResidenteVM nuevo_residente)
        {
            bool dniExiste = await db.Residentes.AnyAsync(r => r.dni == nuevo_residente.dni);

            if (dniExiste) 
            {
                ModelState.AddModelError("dni", "Ya existe usuario con este DNI");

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

            //Crea un Nuevo Usuario de tipo Residente automaticamente
            Usuario usuarioResidente = new Usuario
            {
                tipo = "Residente",
                dni = nuevo_residente.dni,
                nombre = nuevo_residente.nombre + " " + nuevo_residente.apellido,
                email = nuevo_residente.email,
                clave = nuevo_residente.dni.ToString(), // El DNI es la contraseña inicial
            };

            await db.Residentes.AddAsync(residente);
            await db.Usuarios.AddAsync(usuarioResidente);
            await db.SaveChangesAsync();

            return RedirectToAction("Index", "Residentes");
            

        }

        [Authorize(Roles = "Administrador")]
        //Detalle
        public async Task<IActionResult> Detalle(int id)
        {
            Residente? residente = await db.Residentes
                                    .Include(r => r.tipoResidente)
                                    .Include(r => r.estadoResidente)
                                    .FirstOrDefaultAsync(r => r.residenteId == id);

            if (residente == null)
            {
                return NotFound();
            }

            return View(residente);
        }

        //Dar de baja
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<IActionResult> DarDeBaja(int id) //poner boton en la vista Detalle
         {
            Residente? residente = db.Residentes.Find(id);

            try
            {
                if(residente != null && residente.fechaBaja == null)
                {
                    await db.Database.ExecuteSqlInterpolatedAsync($"EXEC DarResidenteDeBaja @residenteId = {residente.residenteId}");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            return RedirectToAction("Index", "Residentes");

        }

        // Modificar
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Modificar(int id)
        {
            Residente? residente = null;
            try
            {
                ViewBag.Tipos = await ObtenerTipos();
                ViewBag.Estados = await ObtenerEstados();
                residente = db.Residentes.Find(id);

                if (residente == null) throw new Exception("Residente no Existente");

            }
            catch (Exception ex)
            {
                return MiRouter.Error(this,ex);
                //return View("Error", ex);
            }


            if (residente == null) return NotFound();

            return View(residente);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<IActionResult> Modificar(Residente residente)
        {
            if (ModelState.IsValid)
            {
                db.Residentes.Update(residente);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Tipos = await ObtenerTipos();
                ViewBag.Estados = await ObtenerEstados();
                return View(residente.residenteId);
            }
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
