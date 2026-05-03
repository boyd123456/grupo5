using Microsoft.AspNetCore.Mvc;
using PlataExpress.Data;
using PlataExpress.Models;


namespace PlataExpress.Controllers
{

    public class UsuarioController : Controller
    {
        private readonly RemesaRepository _repo;

        public UsuarioController(RemesaRepository repo)
        {
            _repo = repo;
        }

        public async Task<IActionResult> PanelUsuario()
        {
            var idUsuarioStr = HttpContext.Session.GetString("IdUsuario");

            if (string.IsNullOrEmpty(idUsuarioStr))
                return RedirectToAction("Login", "Auth");

            int idUsuario = Convert.ToInt32(idUsuarioStr);

            var model = new DashboardViewModel
            {
                TotalEnvios = await _repo.ObtenerTotalEnvios(idUsuario),
                TotalMonto = await _repo.ObtenerTotalMonto(idUsuario),
                Saldo = await _repo.ObtenerSaldo(idUsuario),
                UltimosEnvios = await _repo.ObtenerUltimosEnvios(idUsuario)
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
            return View();

        }
        //panel Para recarga de saldo
        public IActionResult RecargarSaldo()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RecargarSaldo(decimal monto)
        {
            var idUsuarioStr = HttpContext.Session.GetString("IdUsuario");

            if (string.IsNullOrEmpty(idUsuarioStr))
                return RedirectToAction("Login", "Auth");

            if (monto <= 0)
            {
                ModelState.AddModelError("", "El monto debe ser mayor a 0");
                return View();
            }

            int idUsuario = Convert.ToInt32(idUsuarioStr);

            await _repo.RecargarSaldo(idUsuario, monto);

            TempData["mensaje"] = "Saldo recargado correctamente 💰";

            return RedirectToAction("PanelUsuario");
        }

    }
}
