using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TCC.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string NomeUsuario { get; set; }
        public string EmailUsuario {get;set;}
        public string Senha {get;set;}        
        public int IdConta {get;set;}
    }
}