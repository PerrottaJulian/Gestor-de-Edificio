using System.ComponentModel.DataAnnotations;

namespace RedBelgrano.Models.EnumModels
{
    public class CategoriaTransaccion
    {
        [Key]
        public int categoriaId { get; set; }
        public string nombre { get; set; }
    }
}
