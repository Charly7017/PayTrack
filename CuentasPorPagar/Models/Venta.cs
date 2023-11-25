using System.ComponentModel.DataAnnotations;

namespace CuentasPorPagar.Models
{
    public class Venta
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="La descripcion de la venta es obligatoria")]
        [Display(Name = "Descripcion de la venta")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria")]
        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "El monto es obligatorio")]
        [Display(Name = "Monto")]
        public decimal Monto { get; set; }
        public int UsuarioId { get; set; } 

    }
}
