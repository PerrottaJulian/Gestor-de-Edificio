using RedBelgrano.Models.EnumModels;
using RedBelgrano.Models;
using System.ComponentModel.DataAnnotations;

namespace RedBelgrano.DataViewModel
{
    public class AñadirTransaccionVM
    {
        [Required(ErrorMessage = "El monto es obligatorio")]
        public double monto { get; set; }

        [Required(ErrorMessage = "El detalle es obligatorio")]
        public string? detalle { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public int tipoId { get; set; }
    }
}
