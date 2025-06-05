using RedBelgrano.Models.EnumModels;
using RedBelgrano.Models;
using System.ComponentModel.DataAnnotations;

namespace RedBelgrano.DataViewModel
{
    public class AñadirTransaccionVM
    {
        [Required(ErrorMessage = "El monto es obligatorio")]
        [Range(0.0, 9999999.99, ErrorMessage = "El monto debe ser mayor a 0")]
        [Display(Name = "Monto")]
        [DataType(DataType.Currency)]
        public decimal monto { get; set; }

        [Required(ErrorMessage = "El detalle es obligatorio")]
        public string? detalle { get; set; }

        [Required(ErrorMessage = "El tipo es obligatorio")]
        public int tipoId { get; set; }
    }
}
