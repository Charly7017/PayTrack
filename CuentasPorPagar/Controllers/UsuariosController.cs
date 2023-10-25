using CuentasPorPagar.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CuentasPorPagar.Controllers
{
    public class UsuariosController : Controller
    {

        private readonly UserManager<Usuario> userManager;
        private readonly SignInManager<Usuario> signInManager;

        public UsuariosController(UserManager<Usuario> userManager,
            SignInManager<Usuario> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }


        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registro(RegistroViewModel modelo) 
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            var usuario = new Usuario
            {
                Email = modelo.Email
            };

            var resultado = await userManager.CreateAsync(usuario,password:modelo.Password);

            if (resultado.Succeeded)
            {
                return RedirectToAction("Index", "Proveedores");
            }
            else
            {
                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError(string.Empty,error.Description);
                }
                return View(modelo);
            }


            


        }


    }
}
