using Microsoft.AspNetCore.Mvc;

namespace PlataExpress.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult PanelUsuario(string NombreDeUsuario)
        {
            var model = new PlataExpress.Models.LoginViewModel { 
                NombreDeUsuario = !string.IsNullOrEmpty(NombreDeUsuario) ? NombreDeUsuario : "Invitado" 
            };
            return View(model);
        }

        public IActionResult EnviarDinero()
        {
            return View();
        }

        public IActionResult MisEnvios()
        {
            return View();
        }

        public IActionResult EstadoOperacion()
        {
            return View();
        }
    }
}
