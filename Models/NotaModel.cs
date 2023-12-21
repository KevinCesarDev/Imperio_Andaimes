using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TCC.Models
{
    public class NotaModel
    {
        public int Id { get; set; }
        [MaxLength(255)]
        public string Nome { get; set; }

        public string CPF_CNPJ { get; set; }

        public string Telefone { get; set; }
        [Column(TypeName = "DATE")]
        public DateTime DataEmissao { get; set; }
        [Column(TypeName = "DATE")]
        public DateTime DataRecolhimento { get; set; }
        [MaxLength(50)]
        public string Cidade { get; set; }
        [MaxLength(50)]
        public string Bairro { get; set; }
        [MaxLength(50)]
        public string Rua { get; set; }
        [MaxLength(50)]
        public string? Complemento { get; set; }

        public int Cep { get; set; }

        public int? Numero { get; set; }
        [Column(TypeName = "decimal(10,2)")]

        public decimal ValorTotal { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? ValorPago { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? TaxaEntrega { get; set; }
        public bool StatusNota { get; set; }

        public bool ValidarNota()
        {
            var notaValida = true;

            if (this.Nome == null ||
            this.CPF_CNPJ == null ||
            this.Telefone == null ||
            this.DataEmissao == null ||
            this.DataRecolhimento == null ||
            this.Cidade == null ||
            this.Bairro == null ||
            this.Rua == null ||
            this.Cep == null ||
            this.ValorTotal == null)
            {
                notaValida = false;
            }

            return notaValida;
        }
    }
}