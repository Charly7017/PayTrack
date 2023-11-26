using CuentasPorPagar.Models;
using CuentasPorPagar.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CuentasPorPagar.Controllers
{
    public class DashBoardController : Controller
    {

        private readonly IRepositorioDashboard repositorioDashboard;
        private readonly IServicioUsuarios servicioUsuarios;

        public DashBoardController(IRepositorioDashboard repositorioDashboard, IServicioUsuarios servicioUsuarios)
        {
            this.repositorioDashboard = repositorioDashboard;
            this.servicioUsuarios = servicioUsuarios;
        }

        public async Task<IActionResult> Index(int modelValue)
        {
            //var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var comprasAnuales = await repositorioDashboard.ObtenerComprasAnuales();
            var comprasMensuales = await repositorioDashboard.ObtenerComprasMensuales();

            //var comprasMensualesAgrupadas = ComprasAgrupadasPorMes(comprasMensuales);
            //var comprasMensualesAgrupadas = comprasMensuales.GroupBy(p => p.Año);
          


            var comprasMensualesAgrupadas = comprasMensuales.GroupBy(p => p.Año);

            


            var modelo = new DashBoardViewModel
            {
                ComprasAnuales=comprasAnuales,
                ModelValue= modelValue,
                Meses=await ObtenerMesesCompras()
            };

            return View(modelo);
        }

 
      
        public async Task<IActionResult> Gastos(int modelValue)
        {
            var gastosAnuales=await repositorioDashboard.ObtenerGastosAnuales();

            var modelo = new DashBoardViewModel
            {
                GastosAnuales = gastosAnuales,
                ModelValue=modelValue
            };


            return View(modelo);
        }


        public async Task<IActionResult> Ventas(int modelValue)
        {
            var ventasAnuales = await repositorioDashboard.ObtenerVentasAnuales();

            var modelo = new DashBoardViewModel
            {
                VentasAnuales = ventasAnuales,
                ModelValue = modelValue
            };


            return View(modelo);
        }


        //private async Task<IEnumerable<CompraMensualTotal>> ComprasAgrupadasPorMes(IEnumerable<CompraMensualTotal> comprasMensuales)
        //{
        //    return comprasMensuales.GroupBy(p=>p.Mes).Select(p=>);
        //}



        private async Task<IEnumerable<SelectListItem>> ObtenerMesesCompras()
        {
            var meses = await repositorioDashboard.ObtenerMeses();
            


            return meses.Select(p => new SelectListItem(p.Mes.ToString(), "10"));
        }


    }

}
