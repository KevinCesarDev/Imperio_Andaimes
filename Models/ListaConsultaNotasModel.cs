using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TCC.Models
{
    public class ListaConsultaNotasModel
    {
        public List<NotaModel> NotasEncontradas { get; set; }
        public string opcBusca { get; set; }
        public string stringBusca { get; set; }
    }
}