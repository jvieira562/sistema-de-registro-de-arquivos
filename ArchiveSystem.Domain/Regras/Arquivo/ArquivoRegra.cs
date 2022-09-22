using ArchiveSystem.Data.Repository;
using ArchiveSystem.Data.UnitOfWork;
using ArchiveSystem.Domain.Exceptions;
using ArchiveSystem.Dtos;
using ArchiveSystem.Dtos.Usuario;
using ArchiveSystem.Models.Entidades;
using System.Text;
using XAct;
using XSystem.Security.Cryptography;

namespace ArchiveSystem.Domain.Regras.Arquivo
{
    public class ArquivoRegra
    {
        private readonly IUnitOfWork _uow;
        private readonly ArquivoRepository _repository;
        private Criptografia _criptografia = new Criptografia();
        public ArquivoRegra(IUnitOfWork uow, ArquivoRepository repository)
        {
            _repository = repository;
            _uow = uow;
        }

        public bool SalvarArquivo(ArquivoModel arquivo, UsuarioArquivoDto usuario)
        {
            if (!arquivo.IsNull() & !usuario.IsNull())
            {
                arquivo.Cod_Arquivo = _criptografia.TransformaEmHash(GerarCodArquivo());
                _uow.BeginTransaction();
                bool resultado = _repository.SalvarArquivo(arquivo, usuario.Cod_Usuario);
                _uow.Commit();
                if(resultado) return true;
            }
            return false;
        }
        public IEnumerable<ArquivoModel> ListarArquivos(UsuarioArquivoDto usuario)
        {
            return _repository.ListarAquivos(usuario.Cod_Usuario).ToList();            
        }
        public bool ExcluirArquivo(string cod_Arquivo, UsuarioArquivoDto usuario)
        {
            _uow.BeginTransaction();
            bool resultado = _repository.ExcluirArquivo(cod_Arquivo, usuario.Cod_Usuario);
            _uow.Commit();
            if (resultado)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public ArquivoModel BuscarArquivo(string? cod_Arquivo, UsuarioArquivoDto usuario)
        {
            if (!cod_Arquivo.IsNull() & !usuario.IsNull())
            {
                return _repository.BuscarArquivo(cod_Arquivo, usuario.Cod_Usuario);  
            }
            return new ArquivoModel();
        }
        private string GerarCodArquivo()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 4)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }
    }
}
