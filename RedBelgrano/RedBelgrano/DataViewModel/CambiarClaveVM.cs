using System.ComponentModel.DataAnnotations;

namespace RedBelgrano.DataViewModel
{
    public class CambiarClaveVM
    {
        [Required]
        [DataType(DataType.Password)]
        public string ClaveActual { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        public string NuevaClave { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("NuevaClave", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmarClave { get; set; }
    }

}

