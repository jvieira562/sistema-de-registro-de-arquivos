using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveSystem.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit(); /** COMMITAR AS MUDANÇAS **/
        void Rollback();/** DESFAZER ALTERAÇÕES CASO ALGO NÃO FUNCIONE CORRETAMENTE **/
        void BeginTransaction(); /** PARA INICIAR A TRANSAÇÃO **/
    }
}
