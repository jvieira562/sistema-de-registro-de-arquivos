using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveSystem.Domain.Exceptions
{
    [Serializable]
    public class UsuarioNaoEncontradoException : Exception
    {
        public UsuarioNaoEncontradoException() { }

        public UsuarioNaoEncontradoException(string message) : base(message) { }

        public UsuarioNaoEncontradoException(string message, Exception inner) : base(message, inner) { }

        public string NomeUsuario { get; }

        public UsuarioNaoEncontradoException(string message, string nomeUsuario) : this(message)
        {
            NomeUsuario = nomeUsuario;
        }
    }
}
