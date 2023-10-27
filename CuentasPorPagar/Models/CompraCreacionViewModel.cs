using Microsoft.AspNetCore.Mvc.Rendering;

namespace CuentasPorPagar.Models
{
    public class CompraCreacionViewModel:Compra
    {
        public IEnumerable<SelectListItem> ProveedorId { get; set; }
        public IEnumerable<SelectListItem> TipoCompraLista { get; set; } = new List<SelectListItem>
        {
            new SelectListItem { Text = "Credito", Value = "Credito" },
            new SelectListItem { Text = "Contado", Value = "Contado" }
        };

    }
}
