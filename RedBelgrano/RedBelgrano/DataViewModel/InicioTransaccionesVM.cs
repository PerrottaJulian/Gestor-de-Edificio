using RedBelgrano.Models;

namespace RedBelgrano.DataViewModel
{
    public class InicioTransaccionesVM
    {
        public double? Reserva {  get; set; }
        public RegistroMensualVM? BalanceMes {  get; set; }
        public RegistroMensualVM? IngresosMes {  get; set; }
        public RegistroMensualVM? GastosMes {  get; set; }
        public Transaccion? UltimaTransaccion {  get; set; }
    }
}
