namespace CuentasPorPagar.Models
{
    public class EstadoReporte
    {
        public DateTime Fecha { get; set; }
        public decimal TotalVentas { get; set; }
        public decimal TotalCompras { get; set; }
        public decimal Utilidad { get; set; }


    }
}
