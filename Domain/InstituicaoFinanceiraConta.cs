namespace PrototipoLeitorOFX.Domain
{
    public class InstituicaoFinanceiraConta
    {
        public int Id { get; set; }
        public string Conta { get; set; }
        public string Tipo { get; set; }

        public InstituicaoFinanceiraConta() { }

        public InstituicaoFinanceiraConta(string Conta, string Tipo)
        {
            this.Conta = Conta;
            this.Tipo = Tipo;
        }
    }
}