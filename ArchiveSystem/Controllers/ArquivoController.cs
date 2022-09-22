using ArchiveSystem.Domain.Exceptions;
using ArchiveSystem.Domain.Regras.Arquivo;
using ArchiveSystem.Dtos.Usuario;
using ArchiveSystem.LoginSessao;
using ArchiveSystem.Models.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Configuration;
using XAct;

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

        public IActionResult Index(UsuarioArquivoDto usuario)
        {
            usuario = _sessao.BuscarIdUsuarioLogado();

            if (usuario != null)
            {
                return View(_arquivoRegra.ListarArquivos(usuario));
            }
            return RedirectToAction("Index", "Home");
            
        }
        public IActionResult NotFound()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SalvarArquivo(IList<IFormFile> listaArquivos)
        {
            UsuarioArquivoDto usuario = _sessao.BuscarIdUsuarioLogado();

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
                        Cod_Usuario = usuario.Cod_Usuario
                    };
                    _arquivoRegra.SalvarArquivo(arquivo, usuario);
                }
                TempData["MenssagemStatusArquivoOk"] = $"Arquivos salvos com sucesso.";
                return RedirectToAction("Index", "Arquivo");
            }
            TempData["MenssagemStatusArquivoErro"] = $"Ocorreu um erro ao salvar seus arquivos.";
            return RedirectToAction("Index", "Usuario");
        }
        [HttpGet("Visualizar/{id}")]
        public IActionResult VisualizarArquivo(string? id, UsuarioArquivoDto usuario)
        {
            usuario = _sessao.BuscarIdUsuarioLogado();
            ArquivoModel arquivo = _arquivoRegra.BuscarArquivo(id, usuario);

            try
            {
                return File(arquivo.Conteudo, arquivo.Tipo);
            }
            catch (Exception)
            {
                return RedirectToAction("NotFound", "Arquivo");
            }            
        }
        [HttpGet("Download/{id}")]
        public IActionResult DownloadArquivo(string? id, UsuarioArquivoDto usuario)
        {
            if (TaLogado())
            {
                usuario = _sessao.BuscarIdUsuarioLogado();
                ArquivoModel arquivo = _arquivoRegra.BuscarArquivo(id, usuario);
                if (arquivo.IsNull())
                {
                    TempData["MenssagemStatusArquivoErro"] = $"O Arquivo com códido " + id + " não foi encontrado!";
                    return RedirectToAction("NotFound", "Arquivo");
                }
                return File(arquivo.Conteudo, arquivo.Tipo, arquivo.Nome);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        [HttpGet("Excluir/{id}")]
        public IActionResult ExcluirArquivo(string id, UsuarioArquivoDto usuario)
        {
            if (TaLogado())
            {
            usuario = _sessao.BuscarIdUsuarioLogado();
            ArquivoModel arquivo = _arquivoRegra.BuscarArquivo(id, usuario);

            if (!string.IsNullOrEmpty(id) & usuario != null)
            {
                bool status = _arquivoRegra.ExcluirArquivo(id, usuario);

                if (status)
                {
                    TempData["MenssagemStatusArquivoOk"] = $"Arquivo " + arquivo.Nome + " foi apagado com sucesso.";
                }
                else
                {
                    TempData["MenssagemStatusArquivoErro"] = $"Não foi possivel apagar " + arquivo.Nome + ".";
                }
            }
            return RedirectToAction("Index", "Arquivo");
            } else
            {
            return RedirectToAction("Index", "Login");
            }
        }
        private bool TaLogado()
        {
            if (_sessao.BuscarSessao().IsNull())
            {
                return false;
            }
            return true;
        }
    }
}                 

