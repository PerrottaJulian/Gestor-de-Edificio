using System.Net.Sockets;

namespace RedBelgrano.Models.EnumModels
{
    public class EstadoTicket
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        // Navegación
        public ICollection<Ticket> Tickets { get; set; }
    }

}
