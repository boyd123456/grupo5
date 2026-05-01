namespace PlataExpress.Models
{
    public class DashboardViewModel
    {
        public int TotalEnvios { get; set; }
        public decimal TotalMonto { get; set; }
        public int EnProceso { get; set; }
        public List<Remesa> UltimosEnvios { get; set; }
    }
}
