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

        public IActionResult EnviarDinero()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EnviarDinero(Remesa model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var idUsuarioStr = HttpContext.Session.GetString("IdUsuario");

            if (string.IsNullOrEmpty(idUsuarioStr))
                return RedirectToAction("Login", "Auth");

            int idOrigen = Convert.ToInt32(idUsuarioStr);

            if (string.IsNullOrEmpty(model.UsuarioDestino))
            {
                ModelState.AddModelError("", "Debe ingresar un usuario destino");
                return View(model);
            }

            var idDestino = await _repo.ObtenerIdUsuarioPorUsuario(model.UsuarioDestino);

            if (idDestino == null)
            {
                ModelState.AddModelError("", "El usuario destino no existe");
                return View(model);
            }
            decimal saldo = await _repo.ObtenerSaldo(idOrigen);

            if (saldo < model.Monto)
            {
                ModelState.AddModelError("", "Saldo insuficiente");
                return View(model);
            }

            try
            {
                await _repo.TransferirDinero(idOrigen, idDestino.Value, model.Monto);

                // 🔴 COMPLETAR TODOS LOS CAMPOS
                model.IdUsuario = idOrigen;
                model.TipoOperacion = "Transferencia";
                model.Nombre = model.UsuarioDestino;
                model.Agencia = "Online";
                model.FechaEnvio = DateTime.Now;
                model.Estado = "Enviado";
                model.Comision = model.Monto * 0.05m;

                // 🔴 VALIDAR SI SE GUARDÓ
                var resultado = await _repo.EnviarRemesaAsync(model);

                if (!resultado)
                {
                    ModelState.AddModelError("", "No se pudo guardar la remesa");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al procesar el envío: " + ex.Message);
                return View(model);
            }

            TempData["mensaje"] = "Envío realizado correctamente 💸";

            return RedirectToAction("MisEnvios", "Remesa");
        }

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
