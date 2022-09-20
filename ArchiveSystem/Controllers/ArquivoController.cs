using ArchiveSystem.Domain.Regras;
using ArchiveSystem.LoginSessao;
using ArchiveSystem.Models.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Configuration;

namespace ArchiveSystem.Controllers
{
    public class ArquivoController : Controller
    {
        private readonly ArquivoRegra _arquivoRegra;
        private readonly ISessao _sessao;
        public ArquivoController(ArquivoRegra arquivoRegra, ISessao sessao)
        {
            _arquivoRegra = arquivoRegra;
            _sessao = sessao;
        }

        public IActionResult Index(UsuarioModel usuario)
        {
            usuario = _sessao.BuscarSessao();

            if (usuario != null)
            {
                return View(_arquivoRegra.ListarArquivos(usuario.Cod_Usuario));
            }
            return RedirectToAction("Index", "Home");
            
        }
        [HttpPost]
        public IActionResult SalvarArquivo(IList<IFormFile> listaArquivos)
        {
            UsuarioModel usuario = _sessao.BuscarSessao();

            if (ModelState.IsValid)
            {
                MemoryStream ms = new MemoryStream();

                foreach (IFormFile file in listaArquivos)
                {
                    file.OpenReadStream().CopyTo(ms);

                    ArquivoModel arquivo = new ArquivoModel()
                    {
                        Nome = file.FileName,
                        Conteudo = ms.ToArray(),
                        Tipo = file.ContentType,
                        fk_Cod_Usuario = usuario.Cod_Usuario
                    };
                    _arquivoRegra.SalvarArquivo(arquivo, usuario);
                }
                TempData["MenssagemStatusArquivoOk"] = $"Arquivos salvos com sucesso.";
                return RedirectToAction("Index", "Arquivo");
            }
            TempData["MenssagemStatusArquivoErro"] = $"Ocorreu um erro ao salvar seus arquivos.";
            return RedirectToAction("Index", "Usuario");
        }
        [HttpGet("BuscarArquivo/{id}")]
        public IActionResult BuscarArquivo(int? id)
        {
            if (id == null | id == 0)
            {
            return NotFound(RedirectToAction("Index", "Usuario"));
            }

            ArquivoModel arquivo =  _arquivoRegra.BuscarArquivoAtravesDoId(id);
            return File(arquivo.Conteudo, arquivo.Tipo, arquivo.Nome);
        }
        [HttpGet("ApagarArquivo/{id}")]
        public IActionResult ApagarArquivo(int id)
        {
            ArquivoModel arquivo = _arquivoRegra.BuscarArquivoAtravesDoId(id);
            if (id == null | id == 0)
            {
                TempData["MenssagemStatusArquivoErro"] = $"Não foi possivel apagar <b>" + arquivo.Nome.ToUpper() + "<b>.";
                NotFound(RedirectToAction("Index", "Arquivo"));
            }
            
            TempData["MenssagemStatusArquivoOk"] = $"Arquivo " + arquivo.Nome.ToUpper() + " foi apagado com sucesso.";
            _arquivoRegra.ExcluirArquivo(id);
            return RedirectToAction("Index", "Arquivo");
        }
    }
}
