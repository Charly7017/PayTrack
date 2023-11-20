using AutoMapper;
using CuentasPorPagar.Models;
using CuentasPorPagar.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Runtime.CompilerServices;

namespace CuentasPorPagar.Controllers
{
    public class ComprasController : Controller
    {

        private readonly IRepositorioCompras repositorioCompras;
        private readonly IRepositorioProveedor repositorioProveedor;
        private readonly IServicioUsuarios servicioUsuarios;
        private readonly IMapper mapper;

        public ComprasController(IRepositorioCompras repositorioCompras, IServicioUsuarios servicioUsuarios,
            IMapper mapper, IRepositorioProveedor repositorioProveedor)
        {
            this.repositorioCompras = repositorioCompras;
            this.servicioUsuarios = servicioUsuarios;
            this.mapper = mapper;
            this.repositorioProveedor = repositorioProveedor;
        }

        public async Task<IActionResult> Index()
        {
            
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var compras = await  repositorioCompras.Obtener(usuarioId);
            var montoTotal = await repositorioCompras.ObtenerMontoTotal();

            //ViewBag.MontoTotal = montoTotal;

            var modelo = new CompraCreacionViewModel
            {
                Compras=compras,
                MontoTotal=montoTotal
            };


            return View(modelo);
        }

        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var modelo = new CompraCreacionViewModel();

            modelo.Proveedores = await ObtenerProveedores(usuarioId);

            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CompraCreacionViewModel compra)
        {
            if (!ModelState.IsValid)
            {
                return View(compra);
            }

            compra.UsuarioId= servicioUsuarios.ObtenerUsuarioId();

            await repositorioCompras.Crear(compra);

            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Actualizar(int id)
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var compra = await repositorioCompras.ObtenerPorId(id, usuarioId);

            if (compra is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }


            var modelo = mapper.Map<CompraCreacionViewModel>(compra);
            modelo.Proveedores = await ObtenerProveedores(usuarioId);

            return View(modelo);
        }


        [HttpPost]
        public async Task<IActionResult> Actualizar(CompraCreacionViewModel compra)
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var compraa = await repositorioCompras.ObtenerPorId(compra.Id, usuarioId);

            if (compraa is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositorioCompras.Actualizar(compra);

            return RedirectToAction("Index");

        }


        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var compra = repositorioCompras.ObtenerPorId(id,usuarioId);

            if (compra is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositorioCompras.Eliminar(id);

            return RedirectToAction("Index");
        }


        //Se obtienen los proveedores pero en forma de SelectListItem
        private async Task<IEnumerable<SelectListItem>> ObtenerProveedores(int usuarioId)
        {
            var proveedores = await repositorioProveedor.Obtener(usuarioId);

            return proveedores.Select(p => new SelectListItem(p.Nombre, p.Id.ToString()));
        }






    }
}
