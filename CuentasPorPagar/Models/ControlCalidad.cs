using System.ComponentModel.DataAnnotations;

namespace CuentasPorPagar.Models
{
	public class ControlCalidad
	{
        public int Id { get; set; }
		[Display(Name = "Seleccione la compra")]
		public int CompraId { get; set; } 
		[Required(ErrorMessage = "La fecha del informe es obligatoria.")]
		[Display(Name = "Fecha del informe")]
		[DataType(DataType.Date)]
		public DateTime Fecha { get; set; } = DateTime.Today;
        [Required(ErrorMessage = "El estado de la compra es obligatorio.")]
		[Display(Name = "Estado de la compra")]
		public string Estado { get; set; }
		public int UsuarioId { get; set; }
        public string Nombre { get; set; }
    }
}
