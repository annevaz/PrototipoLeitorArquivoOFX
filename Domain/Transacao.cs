using System;
using System.Collections.Generic;

namespace PrototipoLeitorOFX.Domain
{
    public class Transacao
    {
        public int Id { get; set; }
        public int Codigo { get; set; }
        public Status Status { get; set; }
        public string Moeda { get; set; }
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
        public List<TransacaoItem> Itens { get; set; }

        public Transacao() { }

        public Transacao(int Codigo, Status Status, string Moeda,
            DateTime DataInicial, DateTime DataFinal,
            List<TransacaoItem> Itens)
        {
            this.Codigo = Codigo;
            this.Status = Status;
            this.Moeda = Moeda;
            this.DataInicial = DataInicial;
            this.DataFinal = DataFinal;
            this.Itens = Itens;
        }
    }
}