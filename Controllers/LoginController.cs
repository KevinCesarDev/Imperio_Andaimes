using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TCC.Database;
using TCC.Models;
using TCC.Repository.Usuario;

namespace TCC.Controllers
{
    public class LoginController : Controller
    {
        public readonly IUsuarioRepository _usuarioRepository;
        public LoginController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ValidarLogin(UsuarioModel usuario)
        {


            if (_usuarioRepository.AutenticacaoValida(usuario.EmailUsuario, usuario.Senha))
            {
                
                var tipoConta = _usuarioRepository.TipoUsuario(usuario.EmailUsuario,usuario.Senha); 
                var nome = _usuarioRepository.NomeUser(usuario.EmailUsuario,usuario.Senha);
                

                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, usuario.EmailUsuario),
                new Claim(ClaimTypes.Name, nome),
                new Claim(ClaimTypes.Role, tipoConta),
                new Claim("StringClaimType", usuario.Senha),
                
            };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(20)
                });

                TempData["Nome"] = nome;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Tentativa de Login Invalida");
                ViewBag.Erro = "Tentativa de Login Invalida";
                TempData["Erro"] = "Tentativa de Login Inválida";
                return View("Index", usuario);
            }
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }

    }
}