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
    }
}
