using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CuentasPorPagar.Models
{
    public class Compra
    {
        public int Id { get; set; }
        [Display(Name ="Proveedor que realizo la compra")]
        public int ProveedorId { get; set; }

        [Required(ErrorMessage = "La descripcion es obligatoria.")]
        [Display(Name = "Descripcion de la compra")]
        public string  Descripcion { get; set; }

        [Required(ErrorMessage = "La fecha de compra es obligatoria.")]
        [Display(Name = "Fecha de Compra")]
        [DataType(DataType.Date)]
        public DateTime FechaCompra { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "El total es obligatorio.")]
        [Display(Name = "Total")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(10,6)")]
        public decimal Total { get; set; }

        [Required(ErrorMessage = "El tipo de compra es obligatorio.")]
        [Display(Name = "Tipo de Compra")]
        public string TipoCompra { get; set; }
        public int UsuarioId { get; set; }
        public string Proveedor { get; set; }
    }
}
