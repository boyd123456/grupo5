namespace PlataExpress.Models
{
    public class AdminUsuario
    {
        public int IdAdmin { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime UltimaConexion { get; set; }
        public bool Activo { get; set; }
    }
}