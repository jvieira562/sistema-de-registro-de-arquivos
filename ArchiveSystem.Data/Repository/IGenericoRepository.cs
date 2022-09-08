using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveSystem.Data.Repository
{
    public interface IGenericoRepository<T> where T : class
    {
        IEnumerable<T> FindAll();
        IEnumerable<T> Find(T t);
        IEnumerable<T> FindOne(int id);
        void Create(T t);
        void Edit(T t);
        void Excluir(int id);
    }
}
