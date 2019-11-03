using System.Collections.Generic;

namespace PrototipoLeitorOFX.Domain
{
    public class InstituicaoFinanceira
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public InstituicaoFinanceiraConta Conta { get; set; }

        public InstituicaoFinanceira() { }

        public InstituicaoFinanceira(string Codigo, string Nome,
            InstituicaoFinanceiraConta Conta)
        {
            this.Codigo = Codigo;
            this.Nome = Nome;
            this.Conta = Conta;
            
        }
    }
}