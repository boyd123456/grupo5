namespace PlataExpress.Models
{
    public class Remesa
    {
        public int Id { get; set; }
        public string TipoOperacion { get; set; }
        public string Nombre { get; set; }
        public string Agencia { get; set; }
        public decimal Monto { get; set; }
        public decimal Comision { get; set; }
        public DateTime FechaEnvio { get; set; }
        public string Estado { get; set; }
    }
}
