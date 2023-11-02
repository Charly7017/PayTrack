using Microsoft.AspNetCore.Mvc;

namespace CuentasPorPagar.Models
{
    public class GastosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
