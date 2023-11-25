using AutoMapper;
using CuentasPorPagar.Models;
using CuentasPorPagar.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Runtime.InteropServices;

namespace CuentasPorPagar.Controllers
{
    public class GastosController : Controller
    {

        private readonly IRepositorioGastos repositorioGastos;
        private readonly IRepositorioProveedor repositorioProveedor;
        private readonly IServicioUsuarios servicioUsuarios;
        private readonly IRepositorioMetodoPago repositorioMetodoPago;
        private readonly IMapper mapper;

        public GastosController(IRepositorioGastos repositorioGastos, IRepositorioProveedor repositorioProveedor,
            IServicioUsuarios servicioUsuarios,IRepositorioMetodoPago repositorioMetodoPago,IMapper mapper)
        {
            this.repositorioGastos = repositorioGastos;
            this.repositorioProveedor = repositorioProveedor;
            this.servicioUsuarios = servicioUsuarios;
            this.repositorioMetodoPago = repositorioMetodoPago;
            this.mapper = mapper;
        }


        public async Task<IActionResult> Index()
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var gastos = await repositorioGastos.Obtener(usuarioId);
            var montoTotal = await repositorioGastos.ObtenerMontoTotal();

            var modelo = new GastoCreacionViewModel
            {
                Gastos= gastos,
                MontoTotal= montoTotal,  
            };


            return View(modelo);
        }

        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();

            var modelo = new GastoCreacionViewModel();

            modelo.Proveedores = await ObtenerProveedores(usuarioId);
            modelo.MetodosPago = await ObtenerMetodosPago();


            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(GastoCreacionViewModel gasto)
        {
            if (!ModelState.IsValid)
            {
                return View(gasto);
            }

            gasto.UsuarioId = servicioUsuarios.ObtenerUsuarioId();

            await repositorioGastos.Crear(gasto);

            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Actualizar(int id)
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var gasto = await repositorioGastos.ObtenerPorId(id,usuarioId);

            if(gasto is null)
            {
                return RedirectToAction("NoEncontrado","Index");
            }

            var modelo = mapper.Map<GastoCreacionViewModel>(gasto);
            modelo.Proveedores = await ObtenerProveedores(usuarioId);
            modelo.MetodosPago = await ObtenerMetodosPago();

            return View(modelo);
            
        }

        [HttpPost]
        public async Task<IActionResult> Actualizar(GastoCreacionViewModel gasto)
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var gastoo = repositorioGastos.ObtenerPorId(gasto.Id,usuarioId);

            if(gastoo is null)
            {
                return RedirectToAction("NoEncontrado","Index");
            }

            await repositorioGastos.Actualizar(gasto);


            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var gasto = repositorioGastos.ObtenerPorId(id, usuarioId);

            if (gasto is null)
            {
                return RedirectToAction("NoEncontrado", "Index");
            }

       
            await repositorioGastos.Eliminar(id);

            return RedirectToAction("Index");

        }


        private async Task<IEnumerable<SelectListItem>> ObtenerProveedores(int usuarioId)
        {
            var proveedores = await repositorioProveedor.Obtener(usuarioId);

            return proveedores.Select(p => new SelectListItem(p.Nombre, p.Id.ToString()));
        }


        private async Task<IEnumerable<SelectListItem>> ObtenerMetodosPago()
        {
            var metodosPago = await repositorioMetodoPago.Obtener();
            return metodosPago.Select(p=>new SelectListItem(p.Medio,p.Id.ToString()));
        }





    }
}
