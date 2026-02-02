using System.ComponentModel.DataAnnotations;

namespace RedBelgrano.DataViewModel
{
    public class CrearPublicacionViewModel
    {
        [Required(ErrorMessage = "El título de la publicación es obligatorio.")]
        [MaxLength(150, ErrorMessage = "El título no puede superar los 150 caracteres.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "El contenido de la publicación no puede estar vacío.")]
        [MinLength(10, ErrorMessage = "El contenido debe superar los 10 caracteres.")]
        [MaxLength(1000, ErrorMessage = "El contenido no puede superar los 1000 caracteres.")]
        public string Contenido { get; set; }

        [Required(ErrorMessage = "Debes seleccionar una categoría para la publicación.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una categoría para la publicación.")]
        public int CategoriaPublicacionId { get; set; }
    }
}
