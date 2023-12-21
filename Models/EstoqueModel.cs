using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCC.Models;

public class EstoqueModel
{
    // public EstoqueModel()
    // {
    //     ProdutoAtivo = true;
    // }
    
    public int Id { get; set; }
    [NotNull]
    public string Nome { get; set; }
    [NotNull]
    public int QuantidadeTotal  { get; set; }
    [NotNull]
    public int Alugados { get; set; }
    [NotNull]
    public int Disponiveis { get; set; }
    [NotNull]
    [Column(TypeName = "decimal(10,2)")] 
    public decimal ValorUnid { get; set; }
    [NotNull]
    public bool ProdutoAtivo { get; set; } 
}