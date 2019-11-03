using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PrototipoLeitorOFX.Domain;
using PrototipoLeitorOFX.ExceptionArquivoOFX;

namespace PrototipoLeitorOFX
{
    class Program
    {
        static void Main(string[] args)
        {
            var arquivoOFX = "movimento_bancario.ofx";

            if (File.Exists(arquivoOFX))
                LerArquivoOFX(arquivoOFX);                
            else
                throw new ArquivoOFXNaoEncontradoException("O arquivo OFX não foi encontrado!");
        }

        static void LerArquivoOFX(string arquivoOFX)
        {
            StreamReader streamReader = File.OpenText(arquivoOFX);

            if (ArquivoImportadoAnteriormente(streamReader))
            {
                throw new ArquivoOFXImportadoAnteriormenteException("O arquivo OFX já foi importado anteriormente!");
            }
            else
            {
                streamReader.DiscardBufferedData();
                streamReader.BaseStream.Seek(0, SeekOrigin.Begin);

                Arquivo arquivo = ObterInformacaoArquivo(streamReader);
                arquivo.Extrato = ObterInformacaoExtrato(streamReader);
                arquivo.Extrato.Transacao = ObterInformacaoTransacao(streamReader, arquivo.Extrato);
                arquivo.Extrato.Transacao.Itens = ObterInformacaoItemTransacao(streamReader);

                Console.WriteLine("Leitura do arquivo realizada!");
            }
        }

        static bool ArquivoImportadoAnteriormente(StreamReader streamReader)
        {
            return ObterCodigoTransacao(streamReader) == 0;
        }

        static int ObterCodigoTransacao(StreamReader streamReader)
        {
            var linha = "";

            while ((linha = streamReader.ReadLine()) != null)
            {
                if (linha.Trim().StartsWith("<TRNUID>"))
                    return ObterNumeroInteiroLinha(linha, "<TRNUID>", "</TRNUID>");

            }

            return 0;
        }

        static Arquivo ObterInformacaoArquivo(StreamReader streamReader) {
            Arquivo arquivo = new Arquivo();
            
            var linha = streamReader.ReadLine().Trim();

            while (!FimInformacaoArquivo(linha))
            {
                switch (ObterDescricaoItemArquivo(linha))
                {
                    case ("OFXHEADER"):
                        arquivo.NumeroVersaoOFX = ObterNumeroInteiroLinha(linha, "OFXHEADER:");
                        break;
                    case ("DATA"):
                        arquivo.TipoConteudo = ObterTextoLinha(linha, "DATA:");
                        break;
                    case ("VERSION"):
                        arquivo.NumeroVersaoDTD = ObterNumeroInteiroLinha(linha, "VERSION:");
                        break;
                    case ("SECURITY"):
                        arquivo.NivelSegurancaAplicacao = ObterTextoLinha(linha, "SECURITY:");
                        break;
                    case ("ENCODING"):
                        arquivo.Enconding = ObterTextoLinha(linha, "ENCODING:");
                        break;
                    case ("CHARSET"):
                        arquivo.Charset = ObterTextoLinha(linha, "CHARSET:");
                        break;
                    case ("COMPRESSION"):
                        arquivo.Compressao = ObterTextoLinha(linha, "COMPRESSION:");
                        break;
                    case ("OLDFILEUID"):
                        arquivo.ArquivoProcessamento = ObterTextoLinha(linha, "OLDFILEUID:");
                        break;
                    case ("NEWFILEUID"):
                        arquivo.UltimoArquivoProcessado = ObterTextoLinha(linha, "NEWFILEUID:");
                        break;
                }

                linha = streamReader.ReadLine().Trim();
            }

            return arquivo;
        }

        static bool FimInformacaoArquivo(string linha) {
            return linha.Trim().StartsWith("<OFX>");
        }

        static string ObterDescricaoItemArquivo(string linha) {
            return linha.Substring(0, linha.IndexOf(':'));
        }

        static string ObterTextoLinha(string linha, string substituir1, string substituir2 = "")
        {
            var texto = linha.Replace(substituir1, "");
            
            if (substituir2 != "")
                texto = texto.Replace(substituir2, "");
            
            return texto.Trim();
        }

        static int ObterNumeroInteiroLinha(string linha, string substituir1, string substituir2 = "")
        {
            var numero = linha.Replace(substituir1, "");

            if (substituir2 != "")
                numero = numero.Replace(substituir2, "");

            return Int32.Parse(numero.Trim());
        }

        static Double ObterNumeroDecimalLinha(string linha, string substituir1, string substituir2 = "")
        {
            var numero = linha.Replace(substituir1, "");

            if (substituir2 != "")
                numero = numero.Replace(substituir2, "");

            return Convert.ToDouble(numero.Trim());
        }

        static DateTime ObterDataHoraLinha(string linha, string substituir1, string substituir2 = "")
        {
            var dataHora = linha.Replace(substituir1, "");

            if (substituir2 != "")
                dataHora = dataHora.Replace(substituir2, "");

            dataHora = dataHora.Trim();

            return new DateTime(
                Int32.Parse(dataHora.Substring(0, 4)),
                Int32.Parse(dataHora.Substring(4, 2)),
                Int32.Parse(dataHora.Substring(6, 2)),
                Int32.Parse(dataHora.Substring(8, 2)),
                Int32.Parse(dataHora.Substring(10, 2)),
                Int32.Parse(dataHora.Substring(12, 2))
            );
        }

        static Extrato ObterInformacaoExtrato(StreamReader streamReader) {
            Extrato extrato = new Extrato();
            extrato.Status = new Status();
            extrato.InstituicaoFinanceira = new InstituicaoFinanceira();

            var linha = streamReader.ReadLine().Trim();

            while (!FimInformacaoExtrato(linha))
            {
                switch (ObterDescricaoItemExtrato(linha))
                {
                    case ("CODE"):
                        extrato.Status.Codigo = ObterNumeroInteiroLinha(linha, "<CODE>", "</CODE>");
                        break;
                    case ("SEVERITY"):
                        extrato.Status.Categoria = ObterTextoLinha(linha, "<SEVERITY>", "</SEVERITY>");
                        break;
                    case ("DTSERVER"):
                        extrato.DataServidor = ObterDataHoraLinha(linha, "<DTSERVER>", "</DTSERVER>");
                        break;
                    case ("LANGUAGE"):
                        extrato.Linguagem = ObterTextoLinha(linha, "<LANGUAGE>", "</LANGUAGE>");
                        break;
                    case ("ORG"):
                        extrato.InstituicaoFinanceira.Nome = ObterTextoLinha(linha, "<ORG>", "</ORG>");
                        break;
                    case ("FID"):
                        extrato.InstituicaoFinanceira.Codigo = ObterTextoLinha(linha, "<FID>", "</FID>");
                        break;
                }

                linha = streamReader.ReadLine().Trim();
            }

            return extrato;
        }

        static bool FimInformacaoExtrato(string linha) {
            return linha.Trim().StartsWith("</SIGNONMSGSRSV1>");
        }

        static string ObterDescricaoItemExtrato(string linha) {
            return linha.Substring(1, linha.IndexOf('>') - 1);
        }

        static Transacao ObterInformacaoTransacao(StreamReader streamReader, Extrato extrato) {
            Transacao transacao = new Transacao();
            transacao.Status = new Status();

            extrato.InstituicaoFinanceira.Conta = new InstituicaoFinanceiraConta();

            var linha = streamReader.ReadLine().Trim();

            while (!FimInformacaoTransacao(linha))
            {
                switch (ObterDescricaoItemExtrato(linha))
                {
                    case ("TRNUID"):
                        transacao.Codigo = ObterNumeroInteiroLinha(linha, "<TRNUID>", "</TRNUID>");
                        break;
                    case ("CODE"):
                        transacao.Status.Codigo = ObterNumeroInteiroLinha(linha, "<CODE>", "</CODE>");
                        break;
                    case ("SEVERITY"):
                        transacao.Status.Categoria = ObterTextoLinha(linha, "<SEVERITY>", "</SEVERITY>");
                        break;
                    case ("CURDEF"):
                        transacao.Moeda = ObterTextoLinha(linha, "<CURDEF>", "</CURDEF>");
                        break;
                    case ("ACCTID"):
                        extrato.InstituicaoFinanceira.Conta.Conta = ObterTextoLinha(linha, "<ACCTID>", "</ACCTID>");
                        break;
                    case ("ACCTTYPE"):
                        extrato.InstituicaoFinanceira.Conta.Tipo = ObterTextoLinha(linha, "<ACCTTYPE>", "</ACCTTYPE>");
                        break;
                    case ("DTSTART"):
                        transacao.DataInicial = ObterDataHoraLinha(linha, "<DTSTART>", "</DTSTART>");
                        break;
                    case ("DTEND"):
                        transacao.DataFinal = ObterDataHoraLinha(linha, "<DTEND>", "</DTEND>");
                        break;
                }

                linha = streamReader.ReadLine().Trim();
            }

            return transacao;
        }

        static bool FimInformacaoTransacao(string linha) {
            return linha.Trim().StartsWith("<STMTTRN>");
        }

        static List<TransacaoItem> ObterInformacaoItemTransacao(StreamReader streamReader) {
            List<TransacaoItem> itensTransacao = new List<TransacaoItem>();

            string linha = streamReader.ReadLine().Trim();

            TransacaoItem transacaoItem = new TransacaoItem();

            while (!FimInformacaoItemTransacao(linha))
            {
                switch (ObterDescricaoItemExtrato(linha))
                {
                    case ("TRNTYPE"):
                        transacaoItem.Tipo = ObterTextoLinha(linha, "<TRNTYPE>", "</TRNTYPE>");
                        break;
                    case ("DTPOSTED"):
                        transacaoItem.Data = ObterDataHoraLinha(linha, "<DTPOSTED>", "</DTPOSTED>");
                        break;
                    case ("TRNAMT"):
                        transacaoItem.Valor = ObterNumeroDecimalLinha(linha, "<TRNAMT>", "</TRNAMT>");
                        break;
                    case ("FITID"):
                        transacaoItem.Codigo = ObterTextoLinha(linha, "<FITID>", "</FITID>");
                        break;
                    case ("CHECKNUM"):
                        transacaoItem.NumeroDocumento = ObterTextoLinha(linha, "<CHECKNUM>", "</CHECKNUM>");
                        break;
                    case ("REFNUM"):
                        transacaoItem.ReferenciaDocumento = ObterTextoLinha(linha, "<REFNUM>", "</REFNUM>");
                        break;
                    case ("MEMO"):
                        transacaoItem.Descricao = ObterTextoLinha(linha, "<MEMO>", "</MEMO>");
                        break;
                }

                if (AdicionarItemTransacao(linha))
                {
                    itensTransacao.Add(transacaoItem);
                    transacaoItem = new TransacaoItem();
                }

                linha = streamReader.ReadLine().Trim();
            }

            return itensTransacao;
        }

        static bool FimInformacaoItemTransacao(string linha) {
            return linha.Trim().StartsWith("</BANKTRANLIST>");
        }

        static bool AdicionarItemTransacao(string linha) {
            return linha.Trim().StartsWith("</STMTTRN>");
        }
    }
}
