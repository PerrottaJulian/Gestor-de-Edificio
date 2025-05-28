using RedBelgrano.Models.EnumModels;
using System.ComponentModel.DataAnnotations;

namespace RedBelgrano.Models
{
    public class Residente
    {
        [Key]
        public int residenteId { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public int dni { get; set; }
        public string email { get; set; }
        public int telefono { get; set; }
        public int piso { get; set; }
        public char departamento { get; set; }

        //Relaciones
        public int tipoRId { get; set; }
        public TipoResidente? tipoResidente { get; set; }
        public int estadoId { get; set; }
        public EstadoResidente? estadoResidente {get; set; }

        //fechas
        public DateTime? fechaIngreso { get; set; }
        public DateTime? fechaBaja { get; set; }


    }
}
