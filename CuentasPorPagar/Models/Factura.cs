using System.ComponentModel.DataAnnotations;

namespace CuentasPorPagar.Models
{
    public class Factura
    {
        public int Id { get; set; }
        [Display(Name ="Venta perteneciente a la factura")]
        public int VentaId { get; set; }
        [Required(ErrorMessage = "La fecha de emision es obligatoria")]
        [Display(Name = "Fecha de emision")]
        public DateTime FechaEmision { get; set; }
        [Required(ErrorMessage = "La fecha de vencimiento es obligatoria")]
        [Display(Name = "Fecha de vencimiento")]
        public DateTime FechaVencimiento { get; set; }
        [Required(ErrorMessage = "El folio es obligatorio")]
        [Display(Name = "Folio")]
        public string Folio { get; set; }

    }
}
