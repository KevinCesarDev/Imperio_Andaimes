using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCC.Models;

namespace TCC.Repository.Usuario
{
    public interface IUsuarioRepository
    {
        bool AutenticacaoValida (string email,string senha);

        string TipoUsuario(string email, string senha);

        string NomeUser(string email, string senha);

        UsuarioModel NovoUsuario(string nome,string email,string senha,string tipoConta);

        UsuarioModel AlterarDados(UsuarioModel user);

        UsuarioModel UsuarioBusca(string email,string senha);
    }
}