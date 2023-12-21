using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.WebEncoders.Testing;
using TCC.Database;
using TCC.Models;
using TCC.Repository.Pedido;

namespace TCC.Repository
{
    public class EstoqueRepository : IEstoqueRepository
    {
        private readonly BancoContext _dataContexto;
        private readonly IPedidoRepository _pedidoRepository;


        public EstoqueRepository(BancoContext dataContexto, IPedidoRepository pedidoRepository)
        {
            _dataContexto = dataContexto;
            _pedidoRepository = pedidoRepository;
        }

        public EstoqueModel BuscarProduto(int idProduto)
        {
            var produto = _dataContexto.EstoqueGeral.Find(idProduto);

            return produto;


        }

        public EstoqueModel BaixaEstoque(PedidoModel pedido)
        {
            var produtoEstoque = _dataContexto.EstoqueGeral.Find(pedido.IdProduto);
            if (produtoEstoque != null)
            {
                if (produtoEstoque.Disponiveis >= pedido.Quantidade)
                {
                    produtoEstoque.Disponiveis -= pedido.Quantidade;
                    produtoEstoque.Alugados += pedido.Quantidade;
                    Console.WriteLine(produtoEstoque.Disponiveis);
                    _dataContexto.EstoqueGeral.Update(produtoEstoque);
                    _pedidoRepository.AdicionarPedido(pedido);
                    _dataContexto.SaveChanges();
                }
            }
            return produtoEstoque;
        }

        public List<EstoqueModel> ListarEstoque()
        {
            return _dataContexto.EstoqueGeral.Where(u => u.ProdutoAtivo == true).ToList();

        }

        public EstoqueModel EditarEstoque(EstoqueModel editProduto)
        {
             var prodVelho = BuscarProduto(editProduto.Id);

        if (prodVelho != null)
        {
            if (prodVelho.Nome != editProduto.Nome)
            {
                prodVelho.Nome = editProduto.Nome;
            }
            if (prodVelho.QuantidadeTotal != editProduto.QuantidadeTotal)
            {
                if(prodVelho.Alugados<=editProduto.QuantidadeTotal){
                prodVelho.QuantidadeTotal = editProduto.QuantidadeTotal;
                prodVelho.Disponiveis = prodVelho.QuantidadeTotal-prodVelho.Alugados;
                }
            }
            if (prodVelho.ValorUnid != editProduto.ValorUnid)
            {
                prodVelho.ValorUnid = editProduto.ValorUnid;
            }
        }

            _dataContexto.SaveChanges();
            return editProduto;
        }

        public EstoqueModel AdicionarEstoque(EstoqueModel novoProduto)
        {
            _dataContexto.EstoqueGeral.Add(novoProduto);
            _dataContexto.SaveChanges();
            return novoProduto;
        }

        public EstoqueModel ExcluirProduto(int id)
        {
            var produto = BuscarProduto(id);
            produto.ProdutoAtivo = false;
            _dataContexto.EstoqueGeral.Update(produto);
            _dataContexto.SaveChanges();

            return produto;
        }
        public EstoqueModel HabilitarProduto(int id)
        {
            var produto = BuscarProduto(id);
            produto.ProdutoAtivo = true;
            _dataContexto.EstoqueGeral.Update(produto);
            _dataContexto.SaveChanges();

            return produto;
        }

        public List<EstoqueModel> PoucoEstoque()
        {
            var estoque = _dataContexto.EstoqueGeral.Where(u => u.Disponiveis <= 5 && u.ProdutoAtivo == true);
            List<EstoqueModel> listaEstoque = new List<EstoqueModel>();
            listaEstoque = estoque.ToList();

            return listaEstoque;
        }

        public List<EstoqueModel> TodosProdutos()
        {
            return _dataContexto.EstoqueGeral.ToList();
        }

    }
}