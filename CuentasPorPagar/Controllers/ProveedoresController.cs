using AutoMapper;
using CuentasPorPagar.Models;
using CuentasPorPagar.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CuentasPorPagar.Controllers
{
    [Authorize]
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
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
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

			proveedor.UsuarioId = servicioUsuarios.ObtenerUsuarioId();

			var yaExisteProveedor = await repositorioProveedor.Existe(proveedor.RFC,proveedor.UsuarioId);

			if (yaExisteProveedor)
			{
				ModelState.AddModelError(nameof(proveedor.RFC), $"RFC ya existe");
				return View(proveedor);
			}


			await repositorioProveedor.Crear(proveedor);

			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<IActionResult> Actualizar(int id)
		{
			var usuarioId = servicioUsuarios.ObtenerUsuarioId();
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
			var usuarioId = servicioUsuarios.ObtenerUsuarioId();
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
			var usuarioId = servicioUsuarios.ObtenerUsuarioId();
			var proveedor = repositorioProveedor.ObtenerPorId(id,usuarioId);

			if (proveedor is null)
			{
                return RedirectToAction("NoEncontrado", "Home");
            }


			await repositorioProveedor.Eliminar(id);

            return RedirectToAction("Index");

        }


		//[HttpGet]
		//public async Task<IActionResult> VerificarExisteProveedor(string rfc)
		//{
		//	var usuarioId = servicioUsuarios.ObtenerUsuarioId();
		//	var yaExisteProveedor = await repositorioProveedor.Existe(rfc, usuarioId);

		//	if (yaExisteProveedor)
		//	{
		//		return Json($"RFC");
		//	}

		//	return Json(true);

		//}


	}
}
