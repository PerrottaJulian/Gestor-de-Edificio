using Microsoft.AspNetCore.Mvc.Rendering;

namespace RedBelgrano.DataViewModel
{
    public class FiltroTicketViewModel
    {
        public int? EstadoTicketId { get; set; }
        public int? CategoriaTicketId { get; set; }

        public IEnumerable<SelectListItem> Estados { get; set; }
        public IEnumerable<SelectListItem> Categorias { get; set; }

        public List<ListaTicketsViewModel> Tickets { get; set; }
    }
}
