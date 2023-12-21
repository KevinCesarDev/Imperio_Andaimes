using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCC.Models;

namespace TCC.Repository.Pedido
{
    public interface IPedidoRepository
    {
        PedidoModel AdicionarPedido(PedidoModel novoPedido);

        List<PedidoModel> PedidosNota(int idNota);
    }
}