using Microsoft.AspNetCore.Mvc;

namespace CuentasPorPagar.Controllers
{
    public class GastosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
