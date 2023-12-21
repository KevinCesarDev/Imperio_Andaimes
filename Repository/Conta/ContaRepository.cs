using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCC.Database;

namespace TCC.Repository.Conta
{
    public class ContaRepository : IContaRepository
    {
        public readonly BancoContext _bancoContext;
        public ContaRepository( BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public int IdTipoConta(string tipo)
        {   
            var tipoConta = _bancoContext.Contas.FirstOrDefault(u => u.TipoConta == tipo);
            
            return tipoConta.Id;
        }

        public string RoleConta(int id)
        {
            var conta = _bancoContext.Contas.Find(id);
            
            return conta.TipoConta;


        }
    }
}