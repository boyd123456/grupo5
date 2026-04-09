using Microsoft.AspNetCore.Mvc;
using PlataExpress.Models;

namespace PlataExpress.Controllers
{

    public class AdminController : Controller
    {
        public IActionResult AdminMain()
        {
            return View();
        }

        public IActionResult AdminUsuarios()
        {
            var lista = new List<AdminUsuario>();

            return View(lista);
        }

        public IActionResult GestionRemesas()
        {
            return View(new List<Remesa>());
        }
<<<<<<< HEAD
=======

        public IActionResult Pendientes()
        {
            return View(new List<Remesa>());
        }

        public IActionResult Realizados()
        {
            return View(new List<Remesa>());
        }

        public IActionResult Capital()
        {
            return View();
        }

        public IActionResult Reportes()
        {
            return View();
        }

        public IActionResult Configuracion()
        {
            return View();
        }
>>>>>>> 90e015fbae5f307c3b32c15e8ac073d63bd4ff2a
    }
}
