using ArchiveSystem.Dtos;
using ArchiveSystem.Models.Entidades;

namespace ArchiveSystem.LoginSessao
{
    public interface ISessao  
    {
        void CriarSessao(UsuarioModel usuarioModel);
        void DestruirSessao();
        UsuarioModel BuscarSessao();
        void RenovarSessao(UsuarioModel usuarioModel);

    }
}
