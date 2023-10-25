using Microsoft.AspNetCore.Mvc;

namespace CuentasPorPagar.Controllers
{
    public class ComprasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
