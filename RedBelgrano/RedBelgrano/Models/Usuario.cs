using System.ComponentModel.DataAnnotations;

namespace RedBelgrano.Models
{
    public abstract class Usuario
    {
        [Key]
        public int usuarioId { get; set; }
        public int dni { get; set; }
        public string nombre { get; set; }
        public string email { get; set; }

        public void cambiarClave(string NuevaClave)
        {

        }
    }
}
