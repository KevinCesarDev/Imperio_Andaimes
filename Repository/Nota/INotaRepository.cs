using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCC.Models;

namespace TCC.Repository.Nota
{
    public interface INotaRepository
    {
        NotaModel AdicionarNota(NotaModel novaNota);

        int BuscarIdNota(NotaModel nota); 

        List<NotaModel> BuscarNotas(string pesquisa,string opcBusca);

        NotaModel BuscarNota(int id);
        List<NotaModel> NotasAbertas();
        NotaModel BaixarNota(NotaModel nota);
        List<NotaModel> RelatorioData (DateTime inicial,DateTime final);
        List<NotaModel> NotasRecolher();
        List<NotaModel> TodasNotas();
    }
}