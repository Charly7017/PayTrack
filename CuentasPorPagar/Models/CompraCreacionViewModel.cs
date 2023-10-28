using Microsoft.AspNetCore.Mvc.Rendering;

namespace CuentasPorPagar.Models
{
    public class CompraCreacionViewModel:Compra
    {
        public IEnumerable<SelectListItem> Proveedores { get; set; }
        public IEnumerable<SelectListItem> TiposCompras { get; set; } = new List<SelectListItem>
        {
            new SelectListItem { Text = "Credito", Value = "Credito" },
            new SelectListItem { Text = "Contado", Value = "Contado" }
        };

    }
}
