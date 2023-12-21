using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TCC.Database;
using TCC.Models;
using TCC.Repository;

namespace TCC.Controllers;

[Authorize]
public class estoqueController : Controller
{
    public readonly IEstoqueRepository _estoqueRepository;

    public estoqueController(IEstoqueRepository estoqueRepository)
    {
        _estoqueRepository = estoqueRepository;
    }

    public IActionResult Index()
    {
        ViewBag.Page = "Estoque";
        var estoque = _estoqueRepository.TodosProdutos();
        return View(estoque);
    }


    public IActionResult EditarProduto(int id)
    {
        var role = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Role);
        string tipoConta = role?.Value;

        if (tipoConta == "Administrador")
        {
            var estoque = _estoqueRepository.ListarEstoque();

            EstoqueModel produto = new EstoqueModel();

            foreach (var item in estoque)
            {
                if (item.Id == id)
                {
                    produto = item;
                    break;
                }
            }
            return View(produto);
        }
        else
        {
            TempData["Erro"] = "Usuário Não Autorizado";
            return RedirectToAction("Index");
        }
    }

    [Authorize(Roles = "Administrador")]
    [HttpPost]
    public IActionResult ValidarEdit(EstoqueModel produtoNovo)
    {
        _estoqueRepository.EditarEstoque(produtoNovo);
        return RedirectToAction("Index");

    }

    public IActionResult AddProduto()
    {
        var role = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Role);
        string tipoConta = role?.Value;

        if (tipoConta == "Administrador")
        {
            EstoqueModel novoProduto = new EstoqueModel();
            return View(novoProduto);
        }
        else
        {
           TempData["Erro"] = "Usuário Não Autorizado";
            return RedirectToAction("Index");
        }
    }

    [Authorize(Roles = "Administrador")]
    [HttpPost]
    public IActionResult ValidarNovoProd(EstoqueModel novoProduto)
    {
        novoProduto.Disponiveis = novoProduto.QuantidadeTotal;
        _estoqueRepository.AdicionarEstoque(novoProduto);
        return RedirectToAction("Index");
    }

    public IActionResult ExcluirProduto(int id)
    {
        var role = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Role);
        string tipoConta = role?.Value;

        if (tipoConta == "Administrador")
        {
            _estoqueRepository.ExcluirProduto(id);
            return RedirectToAction("Index");
        }
        else
        {
            TempData["Erro"] = "Usuário Não Autorizado";
            return RedirectToAction("Index");
        }

    }
    
    public IActionResult DesabilitarProduto(int id)
    {
        var role = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Role);
        string tipoConta = role?.Value;

        if (tipoConta == "Administrador")
        {
            _estoqueRepository.ExcluirProduto(id);
            return RedirectToAction("Index");
        }
        else
        {
            TempData["Erro"] = "Usuário Não Autorizado";
            return RedirectToAction("Index");
        }

    }

    public IActionResult HabilitarProduto(int id)
    {
        var role = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Role);
        string tipoConta = role?.Value;

        if (tipoConta == "Administrador")
        {
            _estoqueRepository.HabilitarProduto(id);
            return RedirectToAction("Index");
        }
        else
        {
            TempData["Erro"] = "Usuário Não Autorizado";
            return RedirectToAction("Index");
        }

    }
}
