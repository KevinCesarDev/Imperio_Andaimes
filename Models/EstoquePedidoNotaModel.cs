using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TCC.Models
{
    public class EstoquePedidoNotaModel
    {
        public List<EstoqueModel> Estoque {get; set;}

        public List<PedidoModel> Pedidos { get; set; }

        public NotaModel NovaNota {get; set;}

        public ListaConsultaNotasModel ListaNotas { get; set; }

        public decimal SubTotalPedido { get; set; }

        public bool Entrega {get;set;}

        //atribuir a lista de estoque dentro da bag
        public EstoquePedidoNotaModel(List<EstoqueModel> estoque)
        {
            this.Estoque = estoque;
        }
        
        public EstoquePedidoNotaModel()
        {
            
        }
        
        //somar o valor dos pedidos em relação aos valores de estoque 
        public void SomarTotal(){
            decimal somaParcial = 0;
            for(int i=0;i<Pedidos.Count;i++){
                for(int j=0;j<Estoque.Count;j++){
                    if(Pedidos[i].IdProduto == Estoque[j].Id){
                        somaParcial += Estoque[j].ValorUnid*Pedidos[i].Quantidade;
                    }
                }
            }
            this.SubTotalPedido = somaParcial;
        }

        
    }
}