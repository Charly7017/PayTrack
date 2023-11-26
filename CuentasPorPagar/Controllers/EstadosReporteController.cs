using CuentasPorPagar.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace CuentasPorPagar.Controllers
{


    public class EstadosReporteController : Controller
    {
        private readonly IRepositorioEstadoReporte repositorioEstadoReporte;

        public EstadosReporteController(IRepositorioEstadoReporte repositorioEstadoReporte)
        {
            this.repositorioEstadoReporte = repositorioEstadoReporte;
        }

   

        public async Task<IActionResult> Index(int? year)
        {
            var beneficiosDiarios = await repositorioEstadoReporte.ObtenerBeneficioDiario();

            return View(beneficiosDiarios);
        }
    }
}
