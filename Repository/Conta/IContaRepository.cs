using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TCC.Repository.Conta
{
    public interface IContaRepository
    {
        string RoleConta(int id);

        int IdTipoConta (string tipo);
    }
}