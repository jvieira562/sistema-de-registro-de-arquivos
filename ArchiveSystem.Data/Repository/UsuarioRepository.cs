using ArchiveSystem.Models.Entidades;
using ArchiveSystem.Dtos.Usuario;
using Dapper;
using System.Collections.Generic;
using ArchiveSystem.Data.DbConnection;

namespace ArchiveSystem.Data.Repository
{
#pragma warning disable CS8603 // Possível retorno de referência nula.
    public class UsuarioRepository
    {
        private readonly DbSession _session;

        public UsuarioRepository(DbSession session)
        {
            _session = session;
        }

        public void Create(UsuarioModel usuario)
        {
            _session.Connection.Execute(
                @"INSERT INTO   [usuario]
                        (nome,
                        sobrenome,
                        email,
                        senha,
                        perfil)
                VALUES          (@Nome,
                        @Sobrenome,
                        @Email,
                        @Senha,
                        @Perfil) ",
                new { Nome = usuario.Nome,
                    Sobrenome = usuario.Sobrenome,
                    Email = usuario.Email,
                    Senha = usuario.Senha,
                    Perfil = usuario.Perfil },
            _session.Transaction);
        }
        public void Excluir(string cod_Usuario)
        {
            _session.Connection.Execute(
                @"DELETE FROM [usuario]
                WHERE  Cod_usuario = @Cod_Usuario;",
                new { Cod_Usuario = cod_Usuario },
            _session.Transaction);
        }
        public void Edit(UsuarioModel usuario)
        {
            int linhasAfetadas = _session.Connection.Execute(
                @"UPDATE [usuario]
                SET    Nome = @Nome,
                       Sobrenome = @Sobrenome,
                       Email = @Email,
                       Senha = @Senha,
                       Perfil = @Perfil
                WHERE  Cod_usuario = @Cod_Usuario; ",
                new { Nome = usuario.Nome,
                    Sobrenome = usuario.Sobrenome,
                    Email = usuario.Email,
                    Senha = usuario.Senha,
                    Perfil = usuario.Perfil,
                    Cod_Usuario = usuario.Cod_Usuario},
            _session.Transaction);
            Console.WriteLine($"LINHAS AFETADAS: {linhasAfetadas}");
        }
        public UsuarioModel FindOne(string email)
        {
            return _session.Connection.Query<UsuarioModel>(
                @"SELECT cod_usuario,
                       nome,
                       sobrenome,
                       email,
                       perfil
                FROM   [usuario]
                WHERE  email = @Email ",
                new { Email = email },
                _session.Transaction).FirstOrDefault();
        }
        public UsuarioModel BuscarLogin(UsuarioLoginDto usuario)
        {
            return _session.Connection.QueryFirstOrDefault<UsuarioModel>
                (@"SELECT *
                FROM   [usuario]
                WHERE  Email = @Email
                       AND Senha = @Senha;",
                new { Email = usuario.Email,
                    Senha = usuario.Senha },
                _session.Transaction);
        }
    }
}
