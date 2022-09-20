using ArchiveSystem.Models.Enums;

namespace ArchiveSystem.Models.Entidades
{
    public class UsuarioModel
    {
        public UsuarioModel()
        {
            Perfil = (PerfilEnum)2;
        }

        public int Cod_Usuario { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public PerfilEnum? Perfil { get; set; }
        
    }
}