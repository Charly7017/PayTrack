namespace CuentasPorPagar.Models
{
    public class VentaCreacionViewModel:Venta
    {
        public IEnumerable<VentaCreacionViewModel> Ventas { get; set; }
        public decimal MontoTotal { get; set; }

    }
}
