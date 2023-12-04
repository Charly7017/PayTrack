using CuentasPorPagar.Models;
using CuentasPorPagar.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;

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
            //var obj = new EstadoReporteViewModel();
            //var totalUtilidad = ObtenerTotalUtilidad((IEnumerable<EstadoReporteViewModel>)beneficiosDiarios,obj);



            var modelo = new EstadoReporteViewModel
            {
                BeneficiosDiarios = beneficiosDiarios,
                TotalUtilidad = beneficiosDiarios.Sum(p =>
                {
                    if (p.Utilidad > 0)
                    {
                        return p.Utilidad * -1;
                    }
                    return 0;
                }),
                TotalCompras = beneficiosDiarios.Sum(p => p.TotalCompras),
                TotalVentas = beneficiosDiarios.Sum(p=>p.TotalVentas)
            };

            modelo.TotalUtilidad*=-1;

            return View(modelo);
        }


       
    }
}
