using System.ComponentModel.DataAnnotations;

namespace RedBelgrano.Models
{
    public class Usuario
    {
        [Key]
        public int usuarioId { get; set; }

        [Required]
        public string tipo { get; set; }

        [Required]
        public int dni { get; set; }

        [Required]
        public string nombre { get; set; }

        [Required]
        public string email { get; set; }

        private string _clave;
        public string clave //con este bloque de codigo se consigue que se encripte la contraseña automaticamente al darle un valor a la variable clave
        {
            get 
            { 
                return _clave;
            }
            set
            {
                _clave = EncriptarClave(value);
            }
        }

        // estado de usuario(se puede dar de baja)

        //colecciones de un usuario (luego tendria mas)
        public ICollection<Transaccion> Transacciones { get; set; }


        private string EncriptarClave(string clave)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(clave, 13);
        }

        public bool VerificarClave(string claveEntrante)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(claveEntrante, clave);
        }

        public void cambiarClave(string NuevaClave)
        {

        }
    }
}
