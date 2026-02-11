namespace RedBelgrano.DataViewModel
{
    public class ListaTicketsViewModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Categoria { get; set; }
        public string Contenido { get; set; }
        public string Estado { get; set; }
        public string Emisor { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
