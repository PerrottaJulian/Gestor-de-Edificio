using System.ComponentModel.DataAnnotations;

namespace RedBelgrano.Models.EnumModels
{
    public class EstadoResidente
    {
        [Key]
        public int estadoId { get; set; }
        public string estado { get; set; }

        public ICollection<Residente>? Residentes { get; set; }
    }
}
