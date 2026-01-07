namespace RedBelgrano.Models.EnumModels
{
    public class CategoriaPublicacion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        // Navegación
        public ICollection<Publicacion> Publicaciones { get; set; }
    }
}
