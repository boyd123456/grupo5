using System.ComponentModel.DataAnnotations;

namespace PlataExpress.Models
{
    public class Remesa
    {
        public int IdRemesa { get; set; }
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string UsuarioDestino { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "Solo letras permitidas")]
        public string TipoOperacion { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "Solo letras en el nombre")]
        public string Nombre { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "Solo letras en la agencia")]
        public string Agencia { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a 0")]
        public decimal Monto { get; set; }
        public decimal Comision { get; set; }
        public DateTime FechaEnvio { get; set; }
        public string Estado { get; set; }
    }
}
