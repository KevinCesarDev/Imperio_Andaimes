using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TCC.Models;
using TCC.Repository;
using TCC.Repository.Nota;
using TCC.Repository.Pedido;

namespace TCC.Controllers
{
    [Authorize]
    public class RelatorioController : Controller
    {
        public readonly INotaRepository _notaRepository;
        public readonly IPedidoRepository _pedidoRepository;
        public readonly IEstoqueRepository _estoqueRepository;


        public RelatorioController(INotaRepository notaRepository, IPedidoRepository pedidoRepository, IEstoqueRepository estoqueRepository)
        {
            _notaRepository = notaRepository;
            _pedidoRepository = pedidoRepository;
            _estoqueRepository = estoqueRepository;
        }
        public IActionResult Index()
        {
            ViewBag.Page = "Relatorio";
            return View();
        }

        [HttpPost]
        public IActionResult GerarRelatorio(DateTime datanicial, DateTime dataFinal)
        {
            ViewBag.Page = "Relatorio";
            RelatorioModel relatorio = new RelatorioModel();
            List<PedidoModel> listProduto = new List<PedidoModel>();
            List<PedidoModel> topProduto = new List<PedidoModel>();
            relatorio.Estoque = _estoqueRepository.TodosProdutos();
            relatorio.Notas = _notaRepository.RelatorioData(datanicial, dataFinal);
            relatorio.Produtos = listProduto;
            relatorio.TopProdutos = topProduto;
            relatorio.DataInicial = datanicial;
            relatorio.DataFinal = dataFinal;

            foreach (var item in relatorio.Notas)
            {
                relatorio.Produtos.AddRange(_pedidoRepository.PedidosNota(item.Id));
            }
            relatorio.ValidarNotas();
            relatorio.CalcValorTotal();
            relatorio.ClassificarTop();


            if (relatorio.Notas != null)
            {
                return View("ResultRelatorio", relatorio);
            }
            else
            {
                return View("Index");
            }

        }

        public IActionResult ResultRelatorio(RelatorioModel relatorio)
        {
            ViewBag.Page = "Relatorio";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}