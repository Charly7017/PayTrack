namespace CuentasPorPagar.Models
{
    public class EstadoReporteViewModel:EstadoReporte
    {
        public IEnumerable<EstadoReporte> BeneficiosDiarios { get; set; }
        public decimal TotalUtilidad { get; set; }
        public new decimal TotalCompras { get; set; }
        public new decimal TotalVentas { get; set; }

    }
}
