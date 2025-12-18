namespace RedBelgrano.DataViewModel
{
    public class PerfilVM
    {
        // Información base del usuario
        public int UsuarioId { get; set; }
        public string TipoUsuario { get; set; }
        public string Nombre { get; set; }
        public int Dni { get; set; }
        public string Email { get; set; }

        // Información de residente (opcional)
        public ResidentePerfilVM? Residente { get; set; }
    }
}
