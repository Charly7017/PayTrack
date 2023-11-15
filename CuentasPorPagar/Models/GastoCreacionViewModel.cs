using Microsoft.AspNetCore.Mvc.Rendering;

namespace CuentasPorPagar.Models
{
    public class GastoCreacionViewModel:Gasto
    {
        public IEnumerable<SelectListItem> Proveedores { get; set; }
        public IEnumerable<SelectListItem> MetodosPago { get; set; }
    }
}
