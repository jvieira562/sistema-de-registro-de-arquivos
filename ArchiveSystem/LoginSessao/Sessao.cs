using ArchiveSystem.Dtos;
using ArchiveSystem.Dtos.Usuario;
using ArchiveSystem.Models.Entidades;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
namespace ArchiveSystem.LoginSessao
{
    internal class Sessao : ISessao
    {
        private readonly IHttpContextAccessor _httpContext;

        public Sessao(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public UsuarioArquivoDto BuscarIdUsuarioLogado()
        {
            string sessao = _httpContext.HttpContext.Session.GetString("UsuarioLogado");

            if (string.IsNullOrEmpty(sessao))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<UsuarioArquivoDto>(sessao);
        }

        public UsuarioModel BuscarSessao()
        {
            string sessao = _httpContext.HttpContext.Session.GetString("UsuarioLogado");

            if (string.IsNullOrEmpty(sessao))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<UsuarioModel>(sessao);
        }

        public void CriarSessao(UsuarioModel usuarioModel)            
        {
            string usuarioSerializado = JsonConvert.SerializeObject(usuarioModel);
            _httpContext.HttpContext.Session.SetString("UsuarioLogado", usuarioSerializado);
        }   

        public void DestruirSessao()
        {
            _httpContext.HttpContext.Session.Remove("UsuarioLogado");
        }

        public void RenovarSessao(UsuarioModel usuarioModel)
        {
            DestruirSessao();
            CriarSessao(usuarioModel);
        }
    }
}
