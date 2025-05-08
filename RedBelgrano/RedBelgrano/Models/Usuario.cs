namespace RedBelgrano.Models
{
    public abstract class Usuario
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public int dni {  get; set; }
        public string email { get; set; }
        public string clave { get; set; }

        public void cambiarClave(string NuevaClave)
        {

        }
    }
}
