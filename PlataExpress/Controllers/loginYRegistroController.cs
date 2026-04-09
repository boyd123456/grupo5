using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
using PlataExpress.Data;
=======
>>>>>>> 90e015fbae5f307c3b32c15e8ac073d63bd4ff2a
using PlataExpress.Models;

namespace PlataExpress.Controllers
{
    public class loginYRegistroController : Controller
    {
<<<<<<< HEAD
        private readonly loginyRegisterRepositori _repo;

        public loginYRegistroController(loginyRegisterRepositori repo)
        {
            _repo = repo;
        }





=======
>>>>>>> 90e015fbae5f307c3b32c15e8ac073d63bd4ff2a
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }



<<<<<<< HEAD


        [HttpPost]
        public async Task<IActionResult> Register(RegisterUsuarios model)
=======
        [HttpPost]
        public IActionResult Register(RegisterUsuarios model)
>>>>>>> 90e015fbae5f307c3b32c15e8ac073d63bd4ff2a
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

<<<<<<< HEAD
            bool existe = await _repo.ExisteUsuarioOCorreoAsync(model.Usuario, model.Correo);

            if (existe)
            {
                ViewBag.Error = "El usuario o correo ya existe.";
                return View(model);
            }

            model.Rol = "Usuario";
            model.FechaRegistro = DateTime.Now;

            await _repo.RegistrarUsuarioAsync(model);
=======
            // AQUÍ MÁS ADELANTE GUARDARÁS EN LA BASE DE DATOS
            // Por ahora solo simulamos registro exitoso
>>>>>>> 90e015fbae5f307c3b32c15e8ac073d63bd4ff2a

            TempData["Mensaje"] = "Usuario registrado correctamente.";
            return RedirectToAction("Login");
        }



<<<<<<< HEAD




=======
>>>>>>> 90e015fbae5f307c3b32c15e8ac073d63bd4ff2a
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


<<<<<<< HEAD



        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
=======
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
>>>>>>> 90e015fbae5f307c3b32c15e8ac073d63bd4ff2a
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

<<<<<<< HEAD
            var usuario = await _repo.ValidarLoginAsync(model.NombreDeUsuario, model.ClaveDeUsuario);

            if (usuario == null)
=======
            // AQUÍ MÁS ADELANTE VALIDARÁS CON LA BASE DE DATOS
            // Por ahora solo simulamos inicio de sesión

            if (model.NombreDeUsuario == "admin" && model.ClaveDeUsuario == "123")
            {
                return RedirectToAction("Index", "Home");
            }
            else if (model.NombreDeUsuario == "usuario" && model.ClaveDeUsuario == "123")
            {
                return RedirectToAction("PanelUsuario", "Usuario", new { NombreDeUsuario = model.NombreDeUsuario });
            }
            else
>>>>>>> 90e015fbae5f307c3b32c15e8ac073d63bd4ff2a
            {
                ViewBag.Error = "Usuario o contraseña incorrectos.";
                return View(model);
            }

<<<<<<< HEAD
            if (usuario.Rol == "Admin")
            {
                return RedirectToAction("AdminMain", "Admin");
            }

            return RedirectToAction("PanelUsuario", "Usuario", new { NombreDeUsuario = usuario.Usuario });
        }
    }
}
=======
            
        }




    }
}
>>>>>>> 90e015fbae5f307c3b32c15e8ac073d63bd4ff2a
