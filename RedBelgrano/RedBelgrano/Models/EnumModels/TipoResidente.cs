using System.ComponentModel.DataAnnotations;

namespace RedBelgrano.Models.EnumModels
{
    public class TipoResidente
    {
        [Key]
        public int tipoRId { get; set; }
        public string tipo {  get; set; }
    }
}
