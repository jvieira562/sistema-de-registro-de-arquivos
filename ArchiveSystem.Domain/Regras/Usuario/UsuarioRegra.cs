using ArchiveSystem.Data.Repository;
using ArchiveSystem.Data.UnitOfWork;
using ArchiveSystem.Dtos.Usuario;
using ArchiveSystem.Models.Entidades;
using System.Text;
using XAct;
using XSystem.Security.Cryptography;

namespace ArchiveSystem.Domain.Regras.Usuario
{
    public class UsuarioRegra
    {
        private readonly IUnitOfWork _uow;
        private readonly UsuarioRepository _repository;

        public UsuarioRegra(IUnitOfWork uow, UsuarioRepository repository)
        {
            _uow = uow;
            _repository = repository;
        }

        public bool ValidaLogin(UsuarioLoginDto usuarioDto)
        {
            usuarioDto.Senha = CriptografaSenha(usuarioDto.Senha);
            if (_repository.BuscarLogin(usuarioDto) != null)
            {
                return true;
            }

            return false;
        }

        public bool SalvarUsuario(UsuarioModel usuario)
        {
            if (usuario != null)
            {
                usuario.Senha = CriptografaSenha(usuario.Senha);
                _uow.BeginTransaction();
                _repository.Create(usuario);
                _uow.Commit();
                return true;
            }
            return false;
        }
        public bool AtualizarUsuario(UsuarioModel usuario)
        {

            if (usuario != null)
            {
                _uow.BeginTransaction();
                _repository.Edit(usuario);
                _uow.Commit();
                return true;
            }
            return false;
        }

        public UsuarioModel BuscarUsuario(string email)
        {
            return _repository.FindOne(email);
        }

        public bool ExcluirUsuario(string cod_Usuario)
        {
            if (!string.IsNullOrEmpty(cod_Usuario))
            {
                _uow.BeginTransaction();
                _repository.Excluir(cod_Usuario);
                _uow.Commit();
                return true;
            }
            return false;
        }
        private string CriptografaSenha(string senha)
        {
            var hashmd5 = new MD5CryptoServiceProvider();
            byte[] senhaCriptografada = Encoding.UTF8.GetBytes(senha);
            return Convert.ToBase64String(senhaCriptografada);
        }
    }
}
