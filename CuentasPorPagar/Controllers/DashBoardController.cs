using CuentasPorPagar.Models;
using CuentasPorPagar.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace CuentasPorPagar.Controllers
{
    public class DashBoardController : Controller
    {

        private readonly IRepositorioDashboard repositorioDashboard;

        public DashBoardController(IRepositorioDashboard repositorioDashboard)
        {
            this.repositorioDashboard = repositorioDashboard;
        }

        public async Task<IActionResult> Index(int modelValue)
        {
            var comprasAnuales = await repositorioDashboard.ObtenerComprasAnuales();
       

            var modelo = new DashBoardViewModel
            {
                ComprasAnuales=comprasAnuales,
                ModelValue= modelValue
            };

            return View(modelo);
        }

 
      
        public IActionResult Gastos()
        {
            return View();
        }


        public IActionResult Ventas()
        {
            return View();
        }






    }

}
