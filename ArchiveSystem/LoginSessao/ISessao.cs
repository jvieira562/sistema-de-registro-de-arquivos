using ArchiveSystem.Models.Entidades;

namespace ArchiveSystem.LoginSessao
{
    public interface ISessao  
    {
        void CriarSessao(UsuarioModel usuario);
        void DestruirSessao();
        UsuarioModel BuscarSessao();
    }
}
