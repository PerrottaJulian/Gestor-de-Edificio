using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RedBelgrano.Context;
using System.ComponentModel.DataAnnotations;

namespace RedBelgrano.DataViewModel
{
    public class AñadirResidenteVM
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        public string apellido {get;set;}

        [Required(ErrorMessage = "El DNI es obligatorio")]
        [Range(1000000, 99999999, ErrorMessage = "El DNI debe tener entre 7 y 8 dígitos")]
        public int dni {get;set;}

        [Required(ErrorMessage = "El Mail es obligatorio")]
        [EmailAddress(ErrorMessage = "Debe ingresar un correo electrónico válido")]
        public string email {get;set;}

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        public int telefono {get;set;}

        [Required(ErrorMessage = "El piso es obligatorio")]
        public int piso {get;set;}

        [Required(ErrorMessage = "El departamento es obligatorio")]
        [RegularExpression("^[A-Z]{1}$", ErrorMessage = "El departamento debe ser una letra mayúscula")]
        public char departamento {get;set;}

        [Required(ErrorMessage = "Debe seleccionar un tipo de residente")]
        public int tipoRId {get;set;}

        [Required(ErrorMessage = "Debe seleccionar un estado")]
        public int estadoId{get;set;}

    }


}
