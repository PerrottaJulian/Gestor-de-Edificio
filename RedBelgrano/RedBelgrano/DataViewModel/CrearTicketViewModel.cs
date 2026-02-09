using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace RedBelgrano.DataViewModel
{
    public class CrearTicketViewModel
    {
        [Required]
        [StringLength(150)]
        public string Titulo { get; set; }

        [Required]
        [StringLength(1000)]
        public string Contenido { get; set; }

        [Required]
        public int CategoriaTicketId { get; set; }

        // Para el combo
        [ValidateNever]
        public IEnumerable<SelectListItem> Categorias { get; set; }
    }
}
