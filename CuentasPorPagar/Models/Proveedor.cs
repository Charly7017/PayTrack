using CuentasPorPagar.Models;
using System.ComponentModel.DataAnnotations;

namespace CuentasPorPagar.Models
{
	public class Proveedor
	{
		public int Id { get; set; }

		[Display(Name = "Nombre del Proveedor")]
		[Required(ErrorMessage = "El nombre del proveedor es obligatorio.")]
		public string Nombre { get; set; }

		[Display(Name = "Correo Electrónico")]
		[Required(ErrorMessage = "El correo electrónico del proveedor es obligatorio.")]
		[EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
		public string Email { get; set; }

		[Display(Name = "Dirección")]
		[Required(ErrorMessage = "La dirección del proveedor es obligatoria.")]
		public string Direccion { get; set; }

		[Display(Name = "Teléfono")]
		[Required(ErrorMessage = "El número de teléfono del proveedor es obligatorio.")]
		[Phone(ErrorMessage = "El número de teléfono no es válido.")]
		public string Telefono { get; set; }
        public int UsuarioId { get; set; }


       


    }






}




