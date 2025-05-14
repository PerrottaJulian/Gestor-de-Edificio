using System.ComponentModel.DataAnnotations;

namespace RedBelgrano.Models
{
    public class Usuario
    {
        [Key]
        public int usuarioId { get; set; }

        [Required]
        public string tipo { get; set; }

        [Required]
        public int dni { get; set; }

        [Required]
        public string nombre { get; set; }

        [Required]
        public string email { get; set; }

        public string clave { get; set; }

        public void cambiarClave(string NuevaClave)
        {

        }
    }
}
