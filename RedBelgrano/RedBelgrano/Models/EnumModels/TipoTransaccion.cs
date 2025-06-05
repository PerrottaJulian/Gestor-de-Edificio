using System.ComponentModel.DataAnnotations;

namespace RedBelgrano.Models.EnumModels
{
    public class TipoTransaccion
    {
        [Key]
        public int tipoTId { get; set; }
        public string nombre { get; set; }

        public ICollection<Transaccion> Transacciones { get; set;}
    }
}
