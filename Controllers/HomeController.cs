using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TCC.Database;
using TCC.Models;
using TCC.Repository;
using TCC.Repository.Conta;
using TCC.Repository.Nota;
using TCC.Repository.Usuario;

namespace TCC.Controllers;

[Authorize]
public class HomeController : Controller
{
    public readonly IEstoqueRepository _estoqueRepository;
    public readonly IUsuarioRepository _usuarioRepository;
    public readonly INotaRepository _notaRepository;
    public readonly IContaRepository _contaRepository;
    public HomeController(IEstoqueRepository estoqueRepository, INotaRepository notaRepository, IUsuarioRepository usuarioRepository, IContaRepository contaRepository)
    {
        _estoqueRepository = estoqueRepository;
        _notaRepository = notaRepository;
        _usuarioRepository = usuarioRepository;
        _contaRepository = contaRepository;

    }
    public IActionResult Index()
    {
        var role = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Role);
        string tipoConta = role?.Value;
        ViewBag.Page = "Home";
        RelatorioModel geral = new RelatorioModel();
        geral.Estoque = _estoqueRepository.PoucoEstoque();
        geral.Notas = _notaRepository.NotasRecolher();
        return View(geral);
    }

    [Authorize(Roles = "Administrador")]
    public IActionResult CadastrarUsuario()
    {
        ViewBag.Page = "Cadastro";

        return View();
    }


    [HttpPost]
    public IActionResult Cadastrar(string nome, string email, string confirmarEmail, string senha, string confirmarSenha, string tipoConta)
    {


        ViewBag.Page = "Cadastro";

        if (email == confirmarEmail && senha == confirmarSenha)
        {
            _usuarioRepository.NovoUsuario(nome, email, senha, tipoConta);
        }

        return View("CadastrarUsuario");
    }

    public IActionResult Usuario()
    {
        ViewBag.Page = "Usuario";
        return View("Usuario");
    }

    public IActionResult AlterarDados()
    {
        UsuarioModel usuario = new UsuarioModel();
        var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
        var senha = User.Claims.FirstOrDefault(c => c.Type == "StringClaimType")?.Value;
        var conta = _usuarioRepository.TipoUsuario(email, senha);
        var idTipoConta = _contaRepository.IdTipoConta(conta);
        var nome = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;

        usuario.EmailUsuario = email;
        usuario.Senha = senha;
        usuario.IdConta = idTipoConta;
        usuario.Id = _usuarioRepository.UsuarioBusca(email, senha).Id;
        usuario.NomeUsuario = nome;

        return View(usuario);
    }


    public IActionResult Alterar(UsuarioModel user, string novaSenha)
    {
        ViewBag.Page = "Usuario";

        var nome = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;

        if ((user.Senha == novaSenha || novaSenha == null) && (user.NomeUsuario == nome))
        {
            return View("Usuario");
        }
        else
        {
            if (novaSenha != null && novaSenha != "")
            {
                user.Senha = novaSenha;
            }

            ViewBag.Nome = user.NomeUsuario;
            System.Console.WriteLine(user.Senha);
            _usuarioRepository.AlterarDados(user);
            return RedirectToAction("Logout", "Login");
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
