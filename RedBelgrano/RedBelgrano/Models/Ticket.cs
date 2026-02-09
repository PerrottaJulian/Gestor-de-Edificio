using RedBelgrano.Models.EnumModels;

namespace RedBelgrano.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        // Emisor
        public int EmisorId { get; set; }
        public Usuario Emisor { get; set; }

        // Contenido
        public string Titulo { get; set; }
        public string Contenido { get; set; }

        // Estado
        public int EstadoTicketId { get; set; }
        public EstadoTicket EstadoTicket { get; set; }

        // Categoría
        public int CategoriaTicketId { get; set; }
        public CategoriaTicket CategoriaTicket { get; set; }

        // Auditoría
        public DateTime FechaCreacion { get; set; }
    }
}
