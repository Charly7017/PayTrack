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
            var comprasAnuales = await repositorioDashboard.ObtenerComprasAnuales();
            var comprasMensuales = await repositorioDashboard.ObtenerComprasMensuales();
            var comprasMensualesAgrupadas = comprasMensuales.GroupBy(p => p.Año);
            //var años = ObtenerAñosCompra(comprasMensualesAgrupadas);
            var años = ObtenerAñosCompra(comprasAnuales);

            var modelo = new DashBoardViewModel
            {
                ComprasAnuales = comprasAnuales,
                ModelValue = modelValue,
                Años = años,
                ComprasMensuales= comprasMensualesAgrupadas.FirstOrDefault().AsEnumerable(),
                //Años=await ObtenerAñosCompra(),
            };

           //var compraPorAño = comprasMensualesAgrupadas.ToDictionary(g => g.Key, g => g.ToList());
            return View(modelo);
        }

        public async Task<IActionResult> Gastos(int modelValue)
        {
            var gastosAnuales = await repositorioDashboard.ObtenerGastosAnuales();
            var gastosMensuales = await repositorioDashboard.ObtenerGastosMensuales();
            var gastosMensualesAgrupados = gastosMensuales.GroupBy(p=>p.Año);
            var años = ObtenerAñosGasto(gastosAnuales);


            var modelo = new DashBoardViewModel
            {
                GastosAnuales = gastosAnuales,
                ModelValue = modelValue,
                Años = años,
                GastosMensuales= gastosMensualesAgrupados.FirstOrDefault().AsEnumerable(),
            };


            return View(modelo);
        }


        public async Task<IActionResult> Ventas(int modelValue)
        {
            var ventasAnuales = await repositorioDashboard.ObtenerVentasAnuales();
            var ventasMensuales = await repositorioDashboard.ObtenerVentasMensuales();
            var ventasMensualesAgrupadas = ventasMensuales.GroupBy(p=>p.Año);
            var años = ObtenerAñosVenta(ventasAnuales);



            var modelo = new DashBoardViewModel
            {
                VentasAnuales = ventasAnuales,
                ModelValue = modelValue,
                Años=años,
                VentasMensuales= ventasMensualesAgrupadas.FirstOrDefault().AsEnumerable(),
            };


            return View(modelo);
        }



      
        //private IEnumerable<SelectListItem> ObtenerAñosCompra(IEnumerable<IGrouping<int, CompraMensualTotal>> comprasMensualesAgrupadas)
        //{
        //    var años = comprasMensualesAgrupadas.Select(p => p.Key).Select(p => new SelectListItem(p.ToString(), p.ToString()));

        //    return años;
        //}

        private IEnumerable<SelectListItem> ObtenerAñosCompra(IEnumerable<CompraAnualTotal> compraAnualTotal)
        {
            var años = compraAnualTotal.Select(p => p.ComprasAño).Select(p => new SelectListItem(p.ToString(), p.ToString()));

            return años;
        }


        private IEnumerable<SelectListItem> ObtenerAñosGasto(IEnumerable<GastoAnualTotal> compraAnualTotal)
        {
            var años = compraAnualTotal.Select(p => p.GastosAño).Select(p => new SelectListItem(p.ToString(), p.ToString()));

            return años;
        }

        private IEnumerable<SelectListItem> ObtenerAñosVenta(IEnumerable<VentaAnualTotal> compraAnualTotal)
        {
            var años = compraAnualTotal.Select(p => p.VentasAño).Select(p => new SelectListItem(p.ToString(), p.ToString()));

            return años;
        }


    }

}
