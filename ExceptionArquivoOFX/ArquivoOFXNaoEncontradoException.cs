using System;

namespace PrototipoLeitorOFX.ExceptionArquivoOFX
{
    public class ArquivoOFXNaoEncontradoException : Exception
    {
        public ArquivoOFXNaoEncontradoException(string mensagem)
        {
            Console.WriteLine(mensagem);
        }
    }
}