using System.ComponentModel.DataAnnotations;

namespace PlataExpress.Models
{
    public class RegisterUsuarios

    {
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "Los nombres son obligatorios.")]
        public string Nombres { get; set; }


        [Required(ErrorMessage = "Los apellidos son obligatorios.")]
        public string Apellidos { get; set; }


        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Correo electrónico inválido.")]
        public string Correo { get; set; }


        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Clave { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "El teléfono debe tener 9 caracteres.")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El DNI es obligatorio.")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "El DNI debe tener 8 caracteres.")]
        public string DNI { get; set; }

        public string Rol { get; set; } = "Usuario";

        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}
