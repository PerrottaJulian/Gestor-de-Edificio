using RedBelgrano.Models.EnumModels;
using RedBelgrano.Models;
using System.ComponentModel.DataAnnotations;

namespace RedBelgrano.DataViewModel
{
    public class TransaccionesVM
    {
        [Required(ErrorMessage = "El monto es obligatorio")]
        [Range(0.0, 9999999.99, ErrorMessage = "El monto debe ser mayor a 0")]
        [Display(Name = "Monto")]
        [DataType(DataType.Currency)]
        public decimal monto { get; set; }

        [Required(ErrorMessage = "El detalle es obligatorio")]
        [Display(Name = "Detalle")]
        public string? detalle { get; set; }

        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "El tipo es obligatorio")]
        public int tipoId { get; set; }


        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "La categoria es obligatoria ")]
        public int categoriaId { get; set; }

        public List<Transaccion> transacciones { get; set; } = new();
    }
}
