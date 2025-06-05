using RedBelgrano.Models.EnumModels;
using System.ComponentModel.DataAnnotations;

namespace RedBelgrano.Models
{
    public class Transaccion
    {
        [Key]
        public int transaccionId { get; set; }
        public decimal monto { get; set; }
        public string? detalle {  get; set; }

        public int administradorId { get; set; }
        public Usuario administrador { get; set; }
        public int tipoId { get; set; }
        public TipoTransaccion tipoTransaccion { get; set; }
        public DateTime fecha { get; set; }

    }
}
