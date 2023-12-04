using Microsoft.AspNetCore.Mvc.Rendering;

namespace CuentasPorPagar.Models
{
    public class DashBoardViewModel
    {
        public IEnumerable<CompraAnualTotal> ComprasAnuales { get; set; }
        public IEnumerable<GastoAnualTotal> GastosAnuales { get; set; }
        public IEnumerable<VentaAnualTotal> VentasAnuales { get; set; }
        public IEnumerable<CompraMensualTotal> ComprasMensuales { get; set; } // Add this line
        public IEnumerable<GastoMensualTotal> GastosMensuales { get; set; }
        public IEnumerable<VentaMensualTotal> VentasMensuales { get; set; }
        public IEnumerable<SelectListItem> Años { get; set; }
        public int ModelValue { get; set; }
    }
}
