using ArchiveSystem.Models.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ArchiveSystem.ViewComponents
{
    public class Menu : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string sessao = HttpContext.Session.GetString("UsuarioLogado");
            if (string.IsNullOrEmpty(sessao))
            {
                return null;
            }
            UsuarioModel usuario = JsonConvert.DeserializeObject<UsuarioModel>(sessao);

            return View(usuario);
        }
    }
}
