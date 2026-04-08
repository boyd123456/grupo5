using Microsoft.AspNetCore.Mvc;
using PlataExpress.Models;

namespace PlataExpress.Controllers
{
    public class loginYRegistroController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }



        [HttpPost]
        public IActionResult Register(RegisterUsuarios model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // AQUÍ MÁS ADELANTE GUARDARÁS EN LA BASE DE DATOS
            // Por ahora solo simulamos registro exitoso

            TempData["Mensaje"] = "Usuario registrado correctamente.";
            return RedirectToAction("Login");
        }



        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // AQUÍ MÁS ADELANTE VALIDARÁS CON LA BASE DE DATOS
            // Por ahora solo simulamos inicio de sesión

            if (model.NombreDeUsuario == "admin" && model.ClaveDeUsuario == "123")
            {
                return RedirectToAction("Index", "Home");
            }
            else if (model.NombreDeUsuario == "usuario" && model.ClaveDeUsuario == "123")
            {
                return RedirectToAction("PanelUsuario", "Usuario");
            }
            else
            {
                ViewBag.Error = "Usuario o contraseña incorrectos.";
                return View(model);
            }

            
        }




    }
}
