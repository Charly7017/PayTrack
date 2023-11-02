using AutoMapper;
using CuentasPorPagar.Models;
using CuentasPorPagar.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CuentasPorPagar.Controllers
{
	public class ControlCalidadController : Controller
	{

        private readonly IServicioUsuarios servicioUsuarios;
        private readonly IRepositorioControlCalidad repositorioControlCalidad;
        private readonly IRepositorioCompras repositorioCompras;
        private readonly IMapper mapper;


        public ControlCalidadController(IServicioUsuarios servicioUsuarios, IRepositorioControlCalidad repositorioControlCalidad,
            IRepositorioCompras repositorioCompras, IMapper mapper)
        {
            this.servicioUsuarios = servicioUsuarios;
            this.repositorioControlCalidad = repositorioControlCalidad;
            this.repositorioCompras = repositorioCompras;
            this.mapper = mapper;
        }


        public async Task<IActionResult> Index()
		{
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var controlCalidad = await repositorioControlCalidad.Obtener(usuarioId);

            return View(controlCalidad);
		}



        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();

            var modelo = new ControlCalidadCreacionViewModel();

            modelo.Compras = await ObtenerCompras(usuarioId);

            return View(modelo);
        }


        [HttpPost]
        public async Task<IActionResult> Crear(ControlCalidadCreacionViewModel controlCalidad)
        {
            if (!ModelState.IsValid)
            {
                return View(controlCalidad);
            }

            controlCalidad.UsuarioId = servicioUsuarios.ObtenerUsuarioId();

            await repositorioControlCalidad.Crear(controlCalidad);

            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Actualizar(int id)
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var controlCalidad = await repositorioControlCalidad.ObtenerPorId(id, usuarioId);

            if (controlCalidad is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }


            var modelo = mapper.Map<ControlCalidadCreacionViewModel>(controlCalidad);
            modelo.Compras = await ObtenerCompras(usuarioId);

            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Actualizar(ControlCalidadCreacionViewModel controlCalidad)
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var controlCalidadd = await repositorioControlCalidad.ObtenerPorId(controlCalidad.Id, usuarioId);

            if (controlCalidadd is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositorioControlCalidad.Actualizar(controlCalidad);

            return RedirectToAction("Index");

        }




        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var controlCalidad = await repositorioControlCalidad.ObtenerPorId(id, usuarioId);

            if (controlCalidad is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositorioControlCalidad.Eliminar(id);

            return RedirectToAction("Index");
        }





        //Se obtienen las compras pero en forma de SelectListItem
        private async Task<IEnumerable<SelectListItem>> ObtenerCompras(int usuarioId)
        {
            var compras = await repositorioCompras.Obtener(usuarioId);

            return compras.Select(p => new SelectListItem(p.Descripcion, p.Id.ToString()));
        }






    }
}
