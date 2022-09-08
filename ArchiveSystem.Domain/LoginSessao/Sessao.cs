using ArchiveSystem.Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveSystem.Domain.LoginSessao
{
    /**DESCONTINUADO**/
    internal class Sessao : IDomainSessao
    {
        //private readonly IHttpContextAcessor

        public UsuarioModel BuscarSessao()
        {
            throw new NotImplementedException();
        }

        public void CriarSessao(UsuarioModel usuario)            
        {
            throw new NotImplementedException();
        }

        public void DestruirSessao()
        {
            throw new NotImplementedException();
        }
    }
}
