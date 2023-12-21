using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace TCC.Models
{
    public class PedidoModel
    {
        public int Id { get; set; }
        [NotNull]
        public int IdProduto { get; set; }
        [NotNull]
        public int IdNota{ get; set;}
        [NotNull]
        public int Quantidade { get; set; }
        public int QtdDias { get; set; }
    }
}