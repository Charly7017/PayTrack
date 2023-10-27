using AutoMapper;
using CuentasPorPagar.Models;
using CuentasPorPagar.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace CuentasPorPagar.Controllers
{
    public class ProveedoresController : Controller
    {

		private readonly IRepositorioProveedor repositorioProveedor;
		private readonly IServicioUsuarios servicioUsuarios;

		public ProveedoresController(IRepositorioProveedor repositorioProveedor, IServicioUsuarios servicioUsuarios)
		{
			this.repositorioProveedor = repositorioProveedor;
			this.servicioUsuarios = servicioUsuarios;
		}


		public async Task<IActionResult> Index()
        {
            var usuarioId = servicioUsuarios.ObtenerIdUsuario();
			var proveedores = await repositorioProveedor.Obtener(usuarioId);

            return View(proveedores);
        }


		[HttpGet]
        public IActionResult Crear()
        {
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Crear(Proveedor proveedor)
		{
			if (!ModelState.IsValid)
			{
				return View(proveedor);
			}

			proveedor.UsuarioId = servicioUsuarios.ObtenerIdUsuario();

			await repositorioProveedor.Crear(proveedor);

			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<IActionResult> Actualizar(int id)
		{
			var usuarioId = servicioUsuarios.ObtenerIdUsuario();
			var proveedor = await repositorioProveedor.ObtenerPorId(id,usuarioId);


            if (proveedor is null)
            {
                return RedirectToAction("Index");
            }


            return View(proveedor);

		}

		[HttpPost]
		public async Task<IActionResult> Actualizar(Proveedor proveedor)
		{
			var usuarioId = servicioUsuarios.ObtenerIdUsuario();
			var proveedorr = repositorioProveedor.ObtenerPorId(proveedor.Id,usuarioId);

			if (proveedorr is null)
			{
				return RedirectToAction("Index");
			}
            



            await repositorioProveedor.Actualizar(proveedor);

			return RedirectToAction("Index");

		}

		[HttpPost]
		public async Task<IActionResult> Eliminar(int id)
		{
			var usuarioId = servicioUsuarios.ObtenerIdUsuario();
			var proveedor = repositorioProveedor.ObtenerPorId(id,usuarioId);

			if (proveedor is null)
			{
                return RedirectToAction("NoEncontrado", "Home");
            }


			await repositorioProveedor.Eliminar(id);

            return RedirectToAction("Index");

        }


	}
}
