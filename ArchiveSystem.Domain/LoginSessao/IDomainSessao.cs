using ArchiveSystem.Models.Entidades;

namespace ArchiveSystem.Domain.LoginSessao
{
    public interface IDomainSessao  
    {
        void CriarSessao(UsuarioModel usuario);
        void DestruirSessao();
        UsuarioModel BuscarSessao();
    }
}
