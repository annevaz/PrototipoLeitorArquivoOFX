namespace PrototipoLeitorOFX.Domain
{
    public class Arquivo
    {
        public int Id { get; set; }
        public int NumeroVersaoOFX { get; set; }
        public string TipoConteudo { get; set; }
        public int NumeroVersaoDTD { get; set; }
        public string NivelSegurancaAplicacao { get; set; }
        public string Enconding { get; set; }
        public string Charset { get; set; }
        public string Compressao { get; set; }
        public string ArquivoProcessamento { get; set; }
        public string UltimoArquivoProcessado { get; set; }
        public Extrato Extrato { get; set; }

        public Arquivo() { }
        
        public Arquivo(int NumeroVersaoOFX, string TipoConteudo, int NumeroVersaoDTD,
            string NivelSegurancaAplicacao, string Enconding, string Charset,
            string Compressao, string ArquivoProcessamento, string UltimoArquivoProcessado,
            Extrato Extrato)
        {
            this.NumeroVersaoOFX = NumeroVersaoOFX;
            this.TipoConteudo = TipoConteudo;
            this.NumeroVersaoDTD = NumeroVersaoDTD;
            this.NivelSegurancaAplicacao = NivelSegurancaAplicacao;
            this.Enconding = Enconding;
            this.Charset = Charset;
            this.Compressao = Compressao;
            this.ArquivoProcessamento = ArquivoProcessamento;
            this.UltimoArquivoProcessado = UltimoArquivoProcessado;
            this.Extrato = Extrato;
        }
    }
}