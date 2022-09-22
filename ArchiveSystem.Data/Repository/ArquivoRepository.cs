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

        public bool SalvarArquivo(ArquivoModel arquivo, string cod_Usuario)
        {
            int linhasAfetadas = 
                _session.Connection.Execute(
                @"INSERT INTO [arquivo]
                    (Cod_Arquivo,
                    Nome,
                    Conteudo,
                    Tipo,
                    Tamanho,
                    Cod_usuario)
                VALUES(@Cod_Arquivo,
                    @Nome,
                    @Conteudo,
                    @Tipo,
                    @Tamanho,
                    @Cod_Usuario) ",
                new { Cod_Arquivo = arquivo.Cod_Arquivo,
                    Nome = arquivo.Nome,
                    Conteudo = arquivo.Conteudo,
                    Tipo = arquivo.Tipo,
                    Tamanho = arquivo.Tamanho,
                    Cod_Usuario = cod_Usuario},
                    _session.Transaction);
            if (linhasAfetadas > 0)
            {
                return true;
            }
            else
            {
                return false;
            }                                                
        }
        public bool ExcluirArquivo(string cod_Arquivo, string cod_Usuario)
        {
            int linhasAfetadas = _session.Connection.Execute(
                @"DELETE FROM [arquivo]
                WHERE  Cod_arquivo = @cod_Arquivo
                       AND Cod_usuario = @cod_Usuario ",
                new { Cod_Arquivo = cod_Arquivo, Cod_Usuario = cod_Usuario },
                _session.Transaction);            
            
            if (linhasAfetadas > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public ArquivoModel BuscarArquivo(string cod_Arquivo, string cod_Usuario)
        {
            return _session.Connection.QueryFirstOrDefault<ArquivoModel>(
                @"SELECT *
                FROM   [arquivo]
                WHERE  cod_arquivo = @cod_Arquivo
                       AND cod_usuario = @cod_Usuario",
                new { Cod_Arquivo = cod_Arquivo, Cod_Usuario = cod_Usuario });
        }
        public IEnumerable<ArquivoModel> ListarAquivos(string cod_Usuario)
        {
            return _session.Connection.Query<ArquivoModel>(
                @"SELECT *
                FROM   [Arquivo]
                WHERE  Cod_Usuario = @cod_Usuario",
                new { Cod_Usuario = cod_Usuario }).ToList();
        }
        private int QuantidadeDeLinhasAfetadas(int linhasAfetadas)
        {
            return linhasAfetadas;
        }
    }
}
