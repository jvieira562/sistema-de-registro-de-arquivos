using ArchiveSystem.Models.Entidades;
using ArchiveSystem.Dtos;
using Dapper;
using System.Collections.Generic;
using ArchiveSystem.Data.DbConnection;
using System.Text;
using System.Security.Cryptography;

namespace ArchiveSystem.Data.Repository
{
    public class UsuarioRepository
    {
        private readonly DbSession _session;

        public UsuarioRepository(DbSession session)
        {
            _session = session;
        }

        public void Create(UsuarioModel usuario)
        {
            _session.Connection.Execute("INSERT INTO [Usuario] " +
                "(Nome, Sobrenome, Email, Senha, Perfil) " +
                "VALUES (@Nome, @Sobrenome, @Email, @Senha, @Perfil)", new
                { Nome = usuario.Nome, Sobrenome = usuario.Sobrenome,
                    Email = usuario.Email, Senha = usuario.Senha, Perfil = usuario.Perfil }, _session.Transaction);
        }

        public void Excluir(int cod_Usuario)
        {
            _session.Connection.Execute("DELETE FROM [Usuario] WHERE" +
                " Cod_Usuario = @Cod_Usuario;", new { Cod_Usuario = cod_Usuario }, _session.Transaction);
        }

        public void Edit(UsuarioModel usuario)
        {
            _session.Connection.Execute("UPDATE [Usuario] SET Nome = @Nome," +
                " Sobrenome = @Sobrenome, Email = @Email, Senha = @Senha, Perfil = @Perfil WHERE" +
                " Cod_Usuario = @Cod_Usuario;", new { Nome = usuario.Nome, Sobrenome = usuario.Sobrenome,
                    Email = usuario.Email, Senha = usuario.Senha, Perfil = usuario.Perfil, Cod_Usuario = usuario.Cod_Usuario}, _session.Transaction);
        }

        public UsuarioModel FindOne(string email)
        {
            return _session.Connection.Query<UsuarioModel>
                ("SELECT Cod_Usuario, Nome, Sobrenome, Email, Perfil FROM [Usuario] WHERE Email = @Email",
                new { Email = email }, _session.Transaction).FirstOrDefault();
        }

        public UsuarioModel BuscarLogin(UsuarioLoginDto usuario)
        {
            return _session.Connection.Query<UsuarioModel>
                ("SELECT * FROM [Usuario] WHERE Email = @Email AND Senha = @Senha;",
                new { Email = usuario.Email, Senha = usuario.Senha },
                _session.Transaction).FirstOrDefault();
        }

    }
}
