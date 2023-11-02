using Microsoft.AspNetCore.Mvc.Rendering;

namespace CuentasPorPagar.Models
{
	public class ControlCalidadCreacionViewModel:ControlCalidad
	{
		public IEnumerable<SelectListItem> Compras { get; set; }
		public IEnumerable<SelectListItem> Estados { get; set; } = new List<SelectListItem>
		{
			new SelectListItem { Text = "Buena", Value = "Buena" },
			new SelectListItem { Text = "Defectuosa", Value = "Defectuosa" }
		};


	}
}
