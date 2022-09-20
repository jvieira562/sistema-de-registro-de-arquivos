
using ArchiveSystem.Data.Repository;
using ArchiveSystem.Data.UnitOfWork;
using ArchiveSystem.Domain.Exceptions;
using ArchiveSystem.Models.Entidades;

namespace ArchiveSystem.Domain.Regras
{
    public class ArquivoRegra
    {
        private readonly IUnitOfWork _uow;
        private readonly ArquivoRepository _repository;

        public ArquivoRegra(IUnitOfWork uow, ArquivoRepository repository)
        {
            _repository = repository;
            _uow = uow; 
        }

        public bool SalvarArquivo(ArquivoModel arquivo, UsuarioModel usuario)
        {

            if (arquivo != null)
            {
                _uow.BeginTransaction();
                _repository.SalvarArquivo(arquivo, usuario);
                _uow.Commit();
                return true;
            }
            return false;
        }

        public IEnumerable<ArquivoModel> ListarArquivos(int cod_Usuario)
        {
            if (cod_Usuario != null)
            {
                return _repository.ListarAquivos(cod_Usuario);
            }
            return null;
        }

        public bool ExcluirArquivo(int cod_Arquivo)
        {
            if (cod_Arquivo != null)
            {
                _uow.BeginTransaction();
                _repository.ExcluirArquivo(cod_Arquivo);
                _uow.Commit();
                return true;
            }
            return false;
        }
        public ArquivoModel BuscarArquivoAtravesDoId(int? cod_Arquivo)
        {

            if(cod_Arquivo == null | cod_Arquivo == 0)
            {
                throw new ArquivoNaoEncontradoException("Arquivo não encontrador."); ; 
            }
            
            return _repository.BuscarArquivoAtravesDoId(cod_Arquivo);    
        }
    }
}
