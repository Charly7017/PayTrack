using CuentasPorPagar.Models;
using CuentasPorPagar.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection.Metadata.Ecma335;

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

        public async Task<IActionResult> Index(int modelValue, int año)
        {
            var comprasAnuales = await repositorioDashboard.ObtenerComprasAnuales();
            var comprasMensuales = await repositorioDashboard.ObtenerComprasMensuales();
            var comprasMensualesAgrupadas = comprasMensuales.GroupBy(p => p.Año);
            var años = ObtenerAñosCompra(comprasAnuales);

            // Check if the request is an AJAX request
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                // Return JSON data for AJAX request
                var comprasMensualesData = (año == 0)
                    ? comprasMensualesAgrupadas.FirstOrDefault().AsEnumerable()
                    : await repositorioDashboard.ObtenerComprasPorAño(año);

                // Return the JSON data
                return Json(comprasMensualesData);
            }
            else
            {
                // Regular request, process as usual
                var modelo = new DashBoardViewModel
                {
                    ComprasAnuales = comprasAnuales,
                    ModelValue = modelValue,
                    Años = años,
                    ComprasMensuales = (año == 0)
                        ? comprasMensualesAgrupadas.FirstOrDefault().AsEnumerable()
                        : await repositorioDashboard.ObtenerComprasPorAño(año),
                };

                return View(modelo);
            }
        }

        public async Task<IActionResult> Gastos(int modelValue,int año)
        {
            var gastosAnuales = await repositorioDashboard.ObtenerGastosAnuales();
            var gastosMensuales = await repositorioDashboard.ObtenerGastosMensuales();
            var gastosMensualesAgrupados = gastosMensuales.GroupBy(p=>p.Año);
            var años = ObtenerAñosGasto(gastosAnuales); 
            
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                // Return JSON data for AJAX request
                var gastosMensualesData = (año == 0)
                    ? gastosMensualesAgrupados.FirstOrDefault().AsEnumerable()
                    : await repositorioDashboard.ObtenerGastosPorAño(año);

                // Return the JSON data
                return Json(gastosMensualesData);
            } 
            else
            {
                var modelo = new DashBoardViewModel
                {
                    GastosAnuales = gastosAnuales,
                    ModelValue = modelValue,
                    Años = años,
                    GastosMensuales = gastosMensualesAgrupados.FirstOrDefault().AsEnumerable(),
                };
                return View(modelo);
            }
         
        }


        public async Task<IActionResult> Ventas(int modelValue,int año)
        {
            var ventasAnuales = await repositorioDashboard.ObtenerVentasAnuales();
            var ventasMensuales = await repositorioDashboard.ObtenerVentasMensuales();
            var ventasMensualesAgrupadas = ventasMensuales.GroupBy(p=>p.Año);
            var años = ObtenerAñosVenta(ventasAnuales);

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                // Return JSON data for AJAX request
                var ventasMensualesData = (año == 0)
                    ? ventasMensualesAgrupadas.FirstOrDefault().AsEnumerable()
                    : await repositorioDashboard.ObtenerVentasPorAño(año);

                // Return the JSON data
                return Json(ventasMensualesData);
            }
            else
            {
                var modelo = new DashBoardViewModel
                {
                    VentasAnuales = ventasAnuales,
                    ModelValue = modelValue,
                    Años = años,
                    VentasMensuales = ventasMensualesAgrupadas.FirstOrDefault().AsEnumerable(),
                };


                return View(modelo);
            }

           
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
