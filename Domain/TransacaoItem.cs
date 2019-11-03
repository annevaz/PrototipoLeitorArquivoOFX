using System;

namespace PrototipoLeitorOFX.Domain
{
    public class TransacaoItem
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Tipo { get; set; }
        public DateTime Data { get; set; }
        public Double Valor { get; set; }
        public string NumeroDocumento { get; set; }
        public string ReferenciaDocumento { get; set; }
        public string Descricao { get; set; }

        public TransacaoItem() { }

        public TransacaoItem(string Codigo, string Tipo, DateTime Data,
            Double Valor, string NumeroDocumento, string ReferenciaDocumento,
            string Descricao)
        {
            this.Codigo = Codigo;
            this.Tipo = Tipo;
            this.Data = Data;
            this.Valor = Valor;
            this.NumeroDocumento = NumeroDocumento;
            this.ReferenciaDocumento = ReferenciaDocumento;
            this.Descricao = Descricao;
        }
    }
}