using Microsoft.AspNetCore.Mvc;
using PlataExpress.Data;
using PlataExpress.Models;

namespace PlataExpress.Controllers
{
    public class RemesaController : Controller
    {
        private readonly RemesaRepository _repo;

        public RemesaController(RemesaRepository repo)
        {
            _repo = repo;
        }

        // FORMULARIO
        public IActionResult EnviarDinero()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EnviarDinero(Remesa model)
        {
            var idUsuarioStr = HttpContext.Session.GetString("IdUsuario");

            if (string.IsNullOrEmpty(idUsuarioStr))
                return RedirectToAction("Login", "Auth");

            model.IdUsuario = Convert.ToInt32(idUsuarioStr);
            model.FechaEnvio = DateTime.Now;
            model.Estado = "Enviado";

            // 💰 AQUÍ se calcula
            model.Comision = model.Monto * 0.05m;

            await _repo.EnviarRemesaAsync(model);


            return RedirectToAction("MisEnvios", "Remesa");
        }

        // HISTORIAL
        public async Task<IActionResult> Historial()
        {
            int idUsuario = Convert.ToInt32(HttpContext.Session.GetString("IdUsuario"));

            var lista = await _repo.ObtenerRemesasPorUsuario(idUsuario);

            return View(lista);
        }
        public async Task<IActionResult> MisEnvios()
        {
            var idUsuarioStr = HttpContext.Session.GetString("IdUsuario");

            if (string.IsNullOrEmpty(idUsuarioStr))
                return RedirectToAction("Login", "Auth");

            int idUsuario = Convert.ToInt32(idUsuarioStr);

            var lista = await _repo.ObtenerRemesasPorUsuario(idUsuario);

            return View(lista);
        }

    }

}
