using ArchiveSystem.Data.DbConnection;
using ArchiveSystem.Models.Entidades;
using Dapper;

namespace ArchiveSystem.Data.Repository
{
    public class ArquivoRepository
    {
        private readonly DbSession _session;

        public ArquivoRepository(DbSession session)
        {
            _session = session;
        }

        public bool SalvarArquivo(ArquivoModel arquivo, UsuarioModel usuario)
        {
            if (arquivo != null & usuario.Cod_Usuario != null)
            {
            _session.Connection.Execute("INSERT INTO [Arquivo] (Nome, Conteudo, Tipo, fk_Cod_Usuario)" +
                "VALUES (@Nome, @Conteudo, @Tipo, @fk_Cod_Usuario)",
                new { Nome = arquivo.Nome, Conteudo = arquivo.Conteudo, Tipo = arquivo.Tipo, fk_Cod_Usuario = usuario.Cod_Usuario}, _session.Transaction);
            return true;                                    
            }
            return false;
        }
        public bool ExcluirArquivo(int cod_Arquivo)
        {
            if (cod_Arquivo != null)
            {
            _session.Connection.Execute("DELETE FROM [Arquivo] WHERE Cod_Arquivo = @Cod_Arquivo",
                new { Cod_Arquivo = cod_Arquivo }, _session.Transaction);
            return true;
            }
            return false;
        }
        public IEnumerable<ArquivoModel> ListarAquivos(int cod_Usuario)
        {
            return _session.Connection.Query<ArquivoModel>(
                "SELECT * FROM [Arquivo] WHERE fk_Cod_Usuario = @cod_Usuario",
                new { cod_Usuario = cod_Usuario }).ToList();
        }
        public ArquivoModel BuscarArquivoAtravesDoId(int? cod_Arquivo)
        {
            return _session.Connection.Query<ArquivoModel>(
                "SELECT * FROM [Arquivo] WHERE Cod_Arquivo = @cod_Arquivo",
                new { cod_Arquivo = cod_Arquivo }).First();
        }
    }
}
