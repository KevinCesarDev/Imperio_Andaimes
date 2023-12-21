using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCC.Database;
using TCC.Models;
using TCC.Repository.Conta;

namespace TCC.Repository.Usuario
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public readonly BancoContext _bancoContext;
        public readonly IContaRepository _contaRepository;
        public UsuarioRepository(BancoContext bancoContext, IContaRepository contaRepository)
        {
            _bancoContext = bancoContext;
            _contaRepository = contaRepository;
        }
        public bool AutenticacaoValida(string email, string senha)
        {
            var usuarioValidado = _bancoContext.Usuarios.FirstOrDefault(u => u.EmailUsuario == email && u.Senha == senha);

            if (usuarioValidado != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string NomeUser(string email, string senha)
        {
            var usuarioValidado = _bancoContext.Usuarios.FirstOrDefault(u => u.EmailUsuario == email && u.Senha == senha);
            return usuarioValidado.NomeUsuario;
        }

        public string TipoUsuario(string email, string senha)
        {
            var usuarioValidado = _bancoContext.Usuarios.FirstOrDefault(u => u.EmailUsuario == email && u.Senha == senha);
            var tipoConta = _contaRepository.RoleConta(usuarioValidado.IdConta);
            return tipoConta;
        }

        public UsuarioModel NovoUsuario(string nome, string email, string senha, string tipoConta)
        {
            UsuarioModel novoUsuario = new UsuarioModel();
            novoUsuario.EmailUsuario = email;
            novoUsuario.Senha = senha;
            novoUsuario.NomeUsuario = nome;
            novoUsuario.IdConta = _bancoContext.Contas.First(u => u.TipoConta == tipoConta).Id;
            _bancoContext.Usuarios.Update(novoUsuario);
            _bancoContext.SaveChanges();

            return novoUsuario;
        }

        public UsuarioModel AlterarDados(UsuarioModel user)
        {   
            
            _bancoContext.Usuarios.Update(user);
            _bancoContext.SaveChanges();

            return user;

        }

        public UsuarioModel UsuarioBusca (string email, string senha)
        {
            var usuarioValidado = _bancoContext.Usuarios.FirstOrDefault(u => u.EmailUsuario == email && u.Senha == senha);
            return usuarioValidado;
        }

    }
}