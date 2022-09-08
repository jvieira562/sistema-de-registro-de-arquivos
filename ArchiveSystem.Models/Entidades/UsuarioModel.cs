using ArchiveSystem.Models.Enums;

namespace ArchiveSystem.Models.Entidades
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public PerfilEnum? Perfil { get; set; }
        public bool ValidaSenha(string senha)
        {
            return Senha == senha;
        }
    }
}