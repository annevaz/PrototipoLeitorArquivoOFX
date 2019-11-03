using System;
using System.Collections.Generic;

namespace PrototipoLeitorOFX.Domain
{
    public class Extrato
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public DateTime DataServidor { get; set; }
        public string Linguagem { get; set; }
        public InstituicaoFinanceira InstituicaoFinanceira { get; set; }
        public Transacao Transacao { get; set; }

        public Extrato() { }

        public Extrato(Status Status, DateTime DataServidor, string Linguagem,
            InstituicaoFinanceira InstituicaoFinanceira, Transacao Transacao)
        {
            this.Status = Status;
            this.DataServidor = DataServidor;
            this.Linguagem = Linguagem;
            this.InstituicaoFinanceira = InstituicaoFinanceira;
            this.Transacao = Transacao;
        }
    }
}