using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveSystem.Domain.Exceptions
{
    [Serializable]
    public class ArquivoNaoEncontradoException : Exception
    {
        public ArquivoNaoEncontradoException() { }

        public ArquivoNaoEncontradoException(string message) : base(message) { }

        public ArquivoNaoEncontradoException(string message, Exception inner) : base(message, inner) { }

        public string Mensagem { get; }

        public ArquivoNaoEncontradoException(string message, string nomeArquivo) : this(message)
        {
            Mensagem = message;
        }
    }
}
