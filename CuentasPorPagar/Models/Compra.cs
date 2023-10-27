using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CuentasPorPagar.Models
{
    public class Compra
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La fecha de compra es obligatoria.")]
        [Display(Name = "Fecha de Compra")]
        [DataType(DataType.Date)]
        public DateTime FechaCompra { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "La fecha de vencimiento es obligatoria.")]
        [Display(Name = "Fecha de Vencimiento")]
        [DataType(DataType.Date)]
        public DateTime FechaVencimiento { get; set; }  = DateTime.Today;

        [Required(ErrorMessage = "El total es obligatorio.")]
        [Display(Name = "Total")]
        //[Range(0.0, Double.MaxValue, ErrorMessage = "El total debe ser un valor positivo.")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(10,6)")]
        public decimal Total { get; set; }

        [Required(ErrorMessage = "El tipo de compra es obligatorio.")]
        [Display(Name = "Tipo de Compra")]
        public string TipoCompra { get; set; }
        public int UsuarioId { get; set; }

    }
}
