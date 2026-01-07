using RedBelgrano.Models.EnumModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RedBelgrano.Models
{
    public class Publicacion
    {
        public int PublicacionId { get; set; }

        public string Titulo { get; set; }
        public string Contenido { get; set; }

        public DateTime FechaCreacion { get; set; }
        public bool Habilitado { get; set; }

        // Relación con categoría
        public int CategoriaPublicacionId { get; set; }
        public CategoriaPublicacion CategoriaPublicacion { get; set; }

        // Relación con usuario (autor)
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
