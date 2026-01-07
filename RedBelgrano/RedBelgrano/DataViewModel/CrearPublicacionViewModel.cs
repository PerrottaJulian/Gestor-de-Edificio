using System.ComponentModel.DataAnnotations;

namespace RedBelgrano.DataViewModel
{
    public class CrearPublicacionViewModel
    {
        [Required]
        [MaxLength(150)]
        public string Titulo { get; set; }

        [Required]
        public string Contenido { get; set; }

        [Required]
        public int CategoriaPublicacionId { get; set; }
    }
}
