using Microsoft.AspNetCore.Mvc;

namespace PlataExpress.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult PanelUsuario()
        {
            return View();
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
