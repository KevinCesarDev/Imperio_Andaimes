using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TCC.Database;
using TCC.Models;
using TCC.Repository.Pedido;

namespace TCC.Repository.Nota
{
    public class NotaRepository : INotaRepository
    {
        public readonly BancoContext _bancoContex;
        public readonly IPedidoRepository _pedidoRepository;
        public readonly IEstoqueRepository _estoqueRepository;

        public NotaRepository(BancoContext bancoContext, IPedidoRepository pedidoRepository, IEstoqueRepository estoqueRepository)
        {
            _bancoContex = bancoContext;
            _pedidoRepository = pedidoRepository;
            _estoqueRepository = estoqueRepository;
        }

        public NotaModel AdicionarNota(NotaModel novaNota)
        {
            novaNota.StatusNota = true;
            _bancoContex.Notas.Add(novaNota);
            _bancoContex.SaveChanges();
            return novaNota;
        }

        public NotaModel BaixarNota(NotaModel nota)
        {
            List<PedidoModel> produtos = new List<PedidoModel>();
            EstoqueModel itemEstoque = new EstoqueModel();

            produtos = _pedidoRepository.PedidosNota(nota.Id);
            Console.WriteLine(nota.StatusNota);
            if(nota.StatusNota){
                foreach(var i in produtos ){
                    itemEstoque = _estoqueRepository.BuscarProduto(i.IdProduto);
                    itemEstoque.Alugados -= i.Quantidade;
                    itemEstoque.Disponiveis += i.Quantidade; 
                    _bancoContex.EstoqueGeral.Update(itemEstoque);
                }
                nota.StatusNota = false;
                _bancoContex.Notas.Update(nota);
            }

            _bancoContex.SaveChanges();
            return nota;
        }

        public int BuscarIdNota(NotaModel nota)
        {
            //busca mediante a chave primaria
            var notaConsulta = _bancoContex.Notas.Find(nota.Id);

            return notaConsulta.Id;
        }

        public NotaModel BuscarNota(int id)
        {
            var nota = _bancoContex.Notas.Find(id);
            return nota;
        }

        public List<NotaModel> BuscarNotas(string pesquisa, string opcBusca)
        {

            List<NotaModel> listaDeBusca = new List<NotaModel>();

            switch (opcBusca)
            {
                case "CPF/CNPJ":
                    var notasCPF = _bancoContex.Notas.Where(u => u.CPF_CNPJ == pesquisa);
                    listaDeBusca = notasCPF.ToList();
                    break;
                case "NNota":
                    var notasId = _bancoContex.Notas.Where(u => u.Id == int.Parse(pesquisa));
                    listaDeBusca = notasId.ToList();
                    break;
                case "Cliente":
                    var notasCliente = _bancoContex.Notas.Where(u => u.Nome.StartsWith(pesquisa));
                    listaDeBusca = notasCliente.ToList();
                    break;
                case "Tel":
                    var notasTel = _bancoContex.Notas.Where(u => u.Telefone == pesquisa);
                    listaDeBusca = notasTel.ToList();
                    break;
                case "CEP":
                    var notasCEP = _bancoContex.Notas.Where(u => u.Cep == int.Parse(pesquisa));
                    listaDeBusca = notasCEP.ToList();
                    break;
            }
            return listaDeBusca;
        }
        public List<NotaModel> NotasAbertas()
        {
            List<NotaModel> abertas = new List<NotaModel>();

            var notas = _bancoContex.Notas.Where(u => u.StatusNota == true);
            abertas = notas.ToList();

            return abertas;
        }

        public List<NotaModel> RelatorioData(DateTime inicial, DateTime final)
        {   
            List<NotaModel> relatorioNotas = new List<NotaModel>();

            var notas = _bancoContex.Notas.Where(u => u.DataEmissao >=inicial && u.DataEmissao <= final);
            relatorioNotas = notas.ToList();
            
            return relatorioNotas;
        }

        public List<NotaModel> NotasRecolher(){
            List<NotaModel> recolherNotas = new List<NotaModel>();

             DateTime now = DateTime.Now;
             TimeSpan intervalo = TimeSpan.FromDays(5);

            var notas = _bancoContex.Notas.Where(u => u.DataRecolhimento > now && u.DataRecolhimento <= now + intervalo && u.StatusNota == true);
            recolherNotas = notas.ToList();

            return recolherNotas;
        }

        public List<NotaModel> TodasNotas(){

            var notas = _bancoContex.Notas.ToList();
            return notas;
        }
    }
}