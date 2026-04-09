using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
=======
using PlataExpress.Models;
>>>>>>> 90e015fbae5f307c3b32c15e8ac073d63bd4ff2a

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

<<<<<<< HEAD
=======
        [HttpGet]
>>>>>>> 90e015fbae5f307c3b32c15e8ac073d63bd4ff2a
        public IActionResult EnviarDinero()
        {
            return View();
        }

<<<<<<< HEAD
        public IActionResult MisEnvios()
        {
            return View();
=======
        [HttpPost]
        public IActionResult EnviarDinero(Remesa remesa)
        {
            remesa.FechaEnvio = DateTime.Now;
            remesa.Estado = "En proceso";

            anotacionBaseDeDatos.Remesas.Add(remesa);

            TempData["mensaje"] = "¡Tu envío se realizó con éxito!";

            return RedirectToAction("MisEnvios");
        }

        public IActionResult MisEnvios()
        {
            var lista = anotacionBaseDeDatos.Remesas;

            return View(lista);
>>>>>>> 90e015fbae5f307c3b32c15e8ac073d63bd4ff2a
        }

        public IActionResult EstadoOperacion()
        {
            return View();
        }
    }
}
