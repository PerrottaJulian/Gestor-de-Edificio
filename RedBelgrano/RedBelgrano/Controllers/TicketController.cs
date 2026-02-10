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
    public class TicketController : Controller
    {
        private readonly AppDBContext _context;

        public TicketController(AppDBContext context)
        {
            _context = context;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        //public IActionResult Index()
        //{
        //    if (User.IsInRole("Residente"))
        //    {
        //        return Crear();
        //    }
        //    else
        //    {
        //        return Tickets();
        //    }
        //}
        // ----------------- Acciones para RESIDENTES --------------
        
        [Authorize(Roles = "Residente")]
        public IActionResult Crear()
        {
            var emisorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var vm = new CrearTicketViewModel
            {
                Categorias = _context.CategoriaTicket
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Nombre
                    })
                    .ToList(),

                MisTickets = _context.Tickets
                    .Include(t => t.EstadoTicket)
                    .Include(t => t.CategoriaTicket)
                    .Where(t => t.EmisorId == emisorId)
                    .OrderByDescending(t => t.FechaCreacion)
                    .Select(t => new ListadoTicketsResidenteVM
                    {
                        Id = t.Id,
                        Titulo = t.Titulo,
                        Categoria = t.CategoriaTicket.Nombre,
                        Estado = t.EstadoTicket.Nombre,
                        FechaCreacion = t.FechaCreacion
                    })
                    .ToList()
            };

            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Residente")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(CrearTicketViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return Crear();
            }

            var emisorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var ticket = new Ticket
            {
                EmisorId = emisorId,
                Titulo = vm.Titulo,
                Contenido = vm.Contenido,
                CategoriaTicketId = vm.CategoriaTicketId,
                EstadoTicketId = 1, // Abierto
                FechaCreacion = DateTime.Now
            };

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            return RedirectToAction("Crear"); // o a una vista de confirmación
        }

        // ------------- Acciones para ENCARGADO --------------

        //[Authorize(Roles = "Encargado,Administrador")]
        public IActionResult Index(FiltroTicketViewModel filtro)
        {
            if (User.IsInRole("Residente"))
            {
                return RedirectToAction("Crear");
            }

            var query = _context.Tickets
                .Include(t => t.Emisor)
                .Include(t => t.EstadoTicket)
                .Include(t => t.CategoriaTicket)
                .AsQueryable();

            if (filtro.EstadoTicketId.HasValue)
                query = query.Where(t => t.EstadoTicketId == filtro.EstadoTicketId);

            if (filtro.CategoriaTicketId.HasValue)
                query = query.Where(t => t.CategoriaTicketId == filtro.CategoriaTicketId);

            var vm = new FiltroTicketViewModel
            {
                EstadoTicketId = filtro.EstadoTicketId,
                CategoriaTicketId = filtro.CategoriaTicketId,

                Estados = _context.EstadoTicket
                    .Select(e => new SelectListItem
                    {
                        Value = e.Id.ToString(),
                        Text = e.Nombre
                    })
                    .ToList(),

                Categorias = _context.CategoriaTicket
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Nombre
                    })
                    .ToList(),

                Tickets = query
                    .OrderByDescending(t => t.FechaCreacion)
                    .Select(t => new ListaTicketsViewModel
                    {
                        Id = t.Id,
                        Titulo = t.Titulo,
                        Categoria = t.CategoriaTicket.Nombre,
                        Estado = t.EstadoTicket.Nombre,
                        Emisor = t.Emisor.nombre, // ajustá según tu Usuario
                        FechaCreacion = t.FechaCreacion
                    })
                    .ToList()
            };

            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Encargado,Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CambiarEstado(int ticketId, int nuevoEstadoId)
        {
            var ticket = await _context.Tickets.FindAsync(ticketId);

            if (ticket == null)
                return NotFound();

            ticket.EstadoTicketId = nuevoEstadoId;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }




    }
}
