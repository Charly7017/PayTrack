﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuentasPorPagar.Models
{
    public class Gasto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "La descripcion del gasto es necesaria")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "La fecha del gasto es obligatoria.")]
        [Display(Name = "Fecha del gasto")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; } = DateTime.Today;
        [Required(ErrorMessage = "El monto es obligatorio.")]
        [Display(Name = "Monto del Gasto")]
        public decimal Monto { get; set; } 
        public int ProveedorId { get; set; }
        public int MetodoPagoId { get; set; }
        public int UsuarioId { get; set; }
        public string Concepto { get; set; }
        public string MetodoPago { get; set; }
        public string Empresa { get; set; }

    }
}
