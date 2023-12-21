using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Threading.Tasks;

namespace TCC.Models
{
    public class RelatorioModel
    {
        public List<NotaModel> Notas { get; set; }
        public List<PedidoModel> Produtos { get; set; }
        public List<EstoqueModel> Estoque { get; set; }
        public decimal ValorTotalVendas { get; set; }
        public List<PedidoModel> TopProdutos { get; set; }
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
        public int NotasAbertas { get; set; }
        public int NotasFechadas { get; set; }

        public void CalcValorTotal()
        {
            foreach (var item in this.Notas)
            {
                ValorTotalVendas += item.ValorTotal;
            }
        }

        public void ValidarNotas(){
            foreach(var item in Notas){
                if(item.StatusNota == true){
                    this.NotasAbertas ++;
                }else{
                    this.NotasFechadas ++;
                }
            }
        }


        public void ClassificarTop()
        {
            List<PedidoModel> pedidos = new List<PedidoModel>();

            var lista = Produtos.GroupBy(p => p.IdProduto).Select(g => new { IdProduto = g.Key, Quantidade = g.Sum(p => p.Quantidade), }).OrderByDescending(item => item.Quantidade).ToList();

            foreach (var item in lista)
            {
                PedidoModel pedido = new PedidoModel();
                pedido.IdProduto = item.IdProduto;
                pedido.Quantidade = item.Quantidade;
                this.TopProdutos.Add(pedido);
            }
        }

    }
}