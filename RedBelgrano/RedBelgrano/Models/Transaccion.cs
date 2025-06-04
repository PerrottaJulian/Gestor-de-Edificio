namespace RedBelgrano.Models
{
    public class Transaccion
    {
        public int transaccionId { get; set; }
        public int administradorId { get; set; }
        public int tipoId { get; set; }
        public double Monto { get; set; }
        public string? detalle {  get; set; }
        public DateTime fecha { get; set; }

    }
}
