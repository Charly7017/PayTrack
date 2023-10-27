using AutoMapper;
using CuentasPorPagar.Models;
using CuentasPorPagar.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace CuentasPorPagar.Controllers
{
    public class ComprasController : Controller
    {

        private readonly IRepositorioCompras repositorioCompras;
        private readonly IServicioUsuarios servicioUsuarios;
        private readonly IMapper mapper;

        public ComprasController(IRepositorioCompras repositorioCompras, IServicioUsuarios servicioUsuarios,
            IMapper mapper)
        {
            this.repositorioCompras = repositorioCompras;
            this.servicioUsuarios = servicioUsuarios;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var usuarioId = servicioUsuarios.ObtenerIdUsuario();
            var compras = await  repositorioCompras.Obtener(usuarioId);

            return View(compras);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            var modelo = new CompraCreacionViewModel();

            return View(new CompraCreacionViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CompraCreacionViewModel compra)
        {
            if (!ModelState.IsValid)
            {
                return View(compra);
            }

            compra.UsuarioId= servicioUsuarios.ObtenerIdUsuario();

            await repositorioCompras.Crear(compra);

            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Actualizar(int id)
        {
            var usuarioId = servicioUsuarios.ObtenerIdUsuario();
            var compra = await repositorioCompras.ObtenerPorId(id, usuarioId);

            var modelo = mapper.Map<CompraCreacionViewModel>(compra);

            return View(modelo);
        }


        [HttpPost]
        public async Task<IActionResult> Actualizar(CompraCreacionViewModel compra)
        {
            await repositorioCompras.Actualizar(compra);

            return RedirectToAction("Index");

        }


        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            var usuarioId = servicioUsuarios.ObtenerIdUsuario();
            var compra = repositorioCompras.ObtenerPorId(id,usuarioId);

            if (compra is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositorioCompras.Eliminar(id);

            return RedirectToAction("Index");
        }



    }
}
