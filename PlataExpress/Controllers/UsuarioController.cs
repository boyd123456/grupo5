using Microsoft.AspNetCore.Mvc;

using PlataExpress.Models;


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


        [HttpGet]

        public IActionResult EnviarDinero()
        {
            return View();
        }


       

        [HttpPost]
        public IActionResult EnviarDinero(Remesa remesa)
        {
            remesa.FechaEnvio = DateTime.Now;
            remesa.Estado = "En proceso";

            

            TempData["mensaje"] = "¡Tu envío se realizó con éxito!";

            return RedirectToAction("PanelUsuario");
        }

        public IActionResult MisEnvios()
        {
            var lista = anotacionBaseDeDatos.Remesas;

            return View(lista);

        }

        public IActionResult EstadoOperacion()
        {
            return View();
        }
    }
}
