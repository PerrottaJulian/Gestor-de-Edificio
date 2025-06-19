using RedBelgrano.Models;

namespace RedBelgrano.DataViewModel
{
    public class ListaTransaccionesVM
    {
        public List<Transaccion> Transacciones { get; set; }
        public int PaginaActual { get; set; }
        public int TotalPaginas { get; set; }
    }
}
