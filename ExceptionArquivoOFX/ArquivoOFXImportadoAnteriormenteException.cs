using System;

namespace PrototipoLeitorOFX.ExceptionArquivoOFX
{
    public class ArquivoOFXImportadoAnteriormenteException : Exception
    {
        public ArquivoOFXImportadoAnteriormenteException(string mensagem)
        {
            Console.WriteLine(mensagem);
        }
    }
}