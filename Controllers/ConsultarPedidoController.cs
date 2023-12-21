using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TCC.Database;
using TCC.Models;
using TCC.Repository;
using TCC.Repository.Nota;
using TCC.Repository.Pedido;

namespace TCC.Controllers;

[Authorize]
public class consultarPedidoController : Controller
{
    public readonly IPedidoRepository _pedidoRepository;
    public readonly INotaRepository _notaRepository;
    public readonly IEstoqueRepository _estoqueRepository;

    public consultarPedidoController(IPedidoRepository pedidoRepository, INotaRepository notaRepository, IEstoqueRepository estoqueRepository)
    {
        _pedidoRepository = pedidoRepository;
        _notaRepository = notaRepository;
        _estoqueRepository = estoqueRepository;
    }

    public IActionResult Index()
    {
        ViewBag.Page = "Consulta";
        List<NotaModel> AllNotas = new List<NotaModel>();
        AllNotas = _notaRepository.TodasNotas();

        return View(AllNotas);
    }

    [HttpPost]
    public IActionResult ConsultaNota(string busca, string opc, DateTime dataInicial, DateTime dataFinal)
    {
        
        ViewBag.Page = "Consulta";

        EstoquePedidoNotaModel estoquePedidoNota = new EstoquePedidoNotaModel();
        ListaConsultaNotasModel consulta = new ListaConsultaNotasModel();

        estoquePedidoNota.ListaNotas = consulta;

        estoquePedidoNota.ListaNotas.stringBusca = busca;
        estoquePedidoNota.ListaNotas.opcBusca = opc;


        if (estoquePedidoNota.ListaNotas.stringBusca != null && estoquePedidoNota.ListaNotas.opcBusca != null && opc != "Periodo")
        {
            estoquePedidoNota.ListaNotas.NotasEncontradas = _notaRepository.BuscarNotas(estoquePedidoNota.ListaNotas.stringBusca, estoquePedidoNota.ListaNotas.opcBusca);
            return View("ResultPesquisa", estoquePedidoNota);

        }
        else if (opc == "Periodo")
        {
            estoquePedidoNota.ListaNotas.NotasEncontradas = _notaRepository.RelatorioData(dataInicial, dataFinal);
            return View("ResultPesquisa", estoquePedidoNota);
        }
        //necessário notificar usuário sobre o erro de busca
        return View("Index");
    }

    public IActionResult ResultPesquisa(EstoquePedidoNotaModel estoquePedidoNota)
    {
        ViewBag.Page = "Consulta";
        return View(estoquePedidoNota);
    }

    public IActionResult VisualizarNota(int id)
    {
        ViewBag.Page = "Consulta";
        EstoquePedidoNotaModel pedidoNota = new EstoquePedidoNotaModel();
        pedidoNota.NovaNota = _notaRepository.BuscarNota(id);
        pedidoNota.Pedidos = _pedidoRepository.PedidosNota(id);
        pedidoNota.Estoque = _estoqueRepository.TodosProdutos();
        return View(pedidoNota);
    }

    [HttpPost]
    public IActionResult BaixarNota(EstoquePedidoNotaModel nota)
    {
        _notaRepository.BaixarNota(nota.NovaNota);
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
