using System.ComponentModel.DataAnnotations;

namespace PlataExpress.Models
{
    public class Usuarios

    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Este Campo es obligatoriuo")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Este Campo es obligatoriuo")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "Este Campo es obligatoriuo")]
        public string correo { get; set; }

        [Required(ErrorMessage = "Este Campo es obligatoriuo")]
        public string contraseña { get; set; }

        [StringLength(9, MinimumLength = 9, ErrorMessage = "EL TELEFONO DEBE CONTENER 9 CARACTERES.")]
        public string Telefono { get; set; }

        public string Pais { get; set; }

        public int RolId { get; set; }
        public bool Estado { get; set; } = true;
        [DataType(DataType.Date)]
        public DateTime FechaNacimineto { get; set; }

        public DateTime FechaCreacion { get; set; } 

        public bool rol  { get; set; } ;
    }
}
