using CuentasPorPagar.Models;
using CuentasPorPagar.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace CuentasPorPagar.Controllers
{
    public class VentasController : Controller
    {
        private readonly IRepositorioVentas repositorioVentas;
        private readonly IServicioUsuarios servicioUsuarios;


        public VentasController(IRepositorioVentas repositorioVentas,IServicioUsuarios servicioUsuarios)
        {
            this.repositorioVentas = repositorioVentas;
            this.servicioUsuarios= servicioUsuarios;
        }

        public async Task<IActionResult> Index()
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var ventas = await repositorioVentas.Obtener(usuarioId);
            var montoTotal = await repositorioVentas.ObtenerMontoTotal();

            var modelo = new VentaCreacionViewModel
            {
                Ventas= ventas,
                MontoTotal= montoTotal
            };

            return View(modelo);
        }



        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Crear(Venta venta)
        {
            if (!ModelState.IsValid)
            {
                return View(venta);
            }
            venta.UsuarioId = servicioUsuarios.ObtenerUsuarioId();

            await repositorioVentas.Crear(venta);


            return RedirectToAction("Index");
        }



        [HttpGet]
        public async Task<IActionResult> Actualizar(int id)
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var venta = await repositorioVentas.ObtenerPorId(id, usuarioId);


            if (venta is null)
            {
                return RedirectToAction("NoEncontrado", "Home");

            }


            return View(venta);

        }

        [HttpPost]
        public async Task<IActionResult> Actualizar(Venta venta)
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var ventaa = repositorioVentas.ObtenerPorId(venta.Id, usuarioId);

            if (ventaa is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }


            await repositorioVentas.Actualizar(venta);

            return RedirectToAction("Index");

        }



        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var venta = await repositorioVentas.ObtenerPorId(id, usuarioId);

            if (venta is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositorioVentas.Eliminar(id);

            return RedirectToAction("Index");
        }






    }
}
