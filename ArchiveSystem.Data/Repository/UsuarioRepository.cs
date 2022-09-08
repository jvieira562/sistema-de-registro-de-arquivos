using ArchiveSystem.Data.UnitOfWork;
using ArchiveSystem.Models.Entidades;
using ArchiveSystem.Models.Dtos;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveSystem.Data.Repository
{
    public class UsuarioRepository : IGenericoRepository<UsuarioModel>
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

        public void Excluir(int id)
        {
            _session.Connection.Execute("DELETE FROM [Usuario] WHERE" +
                " Id = @Id;", new { Id = id }, _session.Transaction);
        }

        public void Edit(UsuarioModel usuario)
        {
            _session.Connection.Execute("UPDATE [Usuario] SET Nome = @Nome," +
                " Sobrenome = @Sobrenome, Email = @Email, Senha = @Senha WHERE" +
                " Id = @Id;", new { Nome = usuario.Nome, Sobrenome = usuario.Sobrenome,
                    Email = usuario.Email, Senha = usuario.Senha, Id = usuario.Id }, _session.Transaction);
        }
        public IEnumerable<UsuarioModel> Find(UsuarioModel usuario)
        {
            return _session.Connection.Query<UsuarioModel>(
                "SELECT * FROM [Usuarios] WHERE Email = @Email;",
                new { usuario.Email }, _session.Transaction);
        }

        public IEnumerable<UsuarioModel> FindAll()
        {
            return _session.Connection.Query<UsuarioModel>("SELECT * FROM [Usuario];",
                null, _session.Transaction);
        }

        public IEnumerable<UsuarioModel> FindOne(int id)
        {
            return _session.Connection.Query<UsuarioModel>(
                "SELECT * FROM [Usuario] WHERE Id = @Id",
                new { Id = id }, _session.Transaction);
        }

        public UsuarioModel BuscarLogin(string email)
        {
            return _session.Connection.Query<UsuarioModel>("SELECT Email, Senha FROM [Usuario]" +
                " WHERE Email = @Email;",
                new { Email = email },
                _session.Transaction).FirstOrDefault();
        }

    }
}
