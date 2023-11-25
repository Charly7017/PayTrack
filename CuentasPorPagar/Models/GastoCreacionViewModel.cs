using Microsoft.AspNetCore.Mvc.Rendering;

namespace CuentasPorPagar.Models
{
    public class GastoCreacionViewModel:Gasto
    {
        public IEnumerable<SelectListItem> Proveedores { get; set; }
        public IEnumerable<SelectListItem> MetodosPago { get; set; }
        public IEnumerable<GastoCreacionViewModel> Gastos { get; set; }
        public decimal MontoTotal { get; set; }
    }
}
