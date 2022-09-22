using ArchiveSystem.Models.Enums;
using System.Globalization;

namespace ArchiveSystem.Dtos.Usuario
{
    public class UsuarioLoginDto
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
