using Microsoft.AspNetCore.Mvc;
using PlataExpress.Data;
using PlataExpress.Models;

namespace PlataExpress.Controllers
{
    public class loginYRegistroController : Controller
    {
        private readonly loginyRegisterRepositori _repo;

        public loginYRegistroController(loginyRegisterRepositori repo)
        {
            _repo = repo;
        }





        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }





        [HttpPost]
        public async Task<IActionResult> Register(RegisterUsuarios model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool existe = await _repo.ExisteUsuarioOCorreoAsync(model.Usuario, model.Correo);

            if (existe)
            {
                ViewBag.Error = "El usuario o correo ya existe.";
                return View(model);
            }

            model.Rol = "Usuario";
            model.FechaRegistro = DateTime.Now;

            await _repo.RegistrarUsuarioAsync(model);

            TempData["Mensaje"] = "Usuario registrado correctamente.";
            return RedirectToAction("Login");
        }







        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }





        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var usuario = await _repo.ValidarLoginAsync(model.NombreDeUsuario, model.ClaveDeUsuario);

            if (usuario == null)
            {
                ViewBag.Error = "Usuario o contraseña incorrectos.";
                return View(model);
            }

            if (usuario.Rol == "Admin")
            {
                return RedirectToAction("AdminMain", "Admin");
            }

            return RedirectToAction("PanelUsuario", "Usuario", new { NombreDeUsuario = usuario.Usuario });
        }
    }
}