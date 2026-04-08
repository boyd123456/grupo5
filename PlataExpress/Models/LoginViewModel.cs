using System.ComponentModel.DataAnnotations;

namespace PlataExpress.Models
{
    public class LoginViewModel

    {
        [Required(ErrorMessage = "El nombre de usuario  es obligatorio")]
        [EmailAddress(ErrorMessage = "Correo electrónico inválido.")]
        public string NombreDeUsuario { get; set; }



        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        public string ClaveDeUsuario { get; set; }
    }
}
