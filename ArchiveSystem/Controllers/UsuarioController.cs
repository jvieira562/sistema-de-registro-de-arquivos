using ArchiveSystem.Domain.Regras.Usuario;
using ArchiveSystem.Dtos.Usuario;
using ArchiveSystem.LoginSessao;
using ArchiveSystem.Models.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XAct;

namespace ArchiveSystem.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioRegra _usuarioRegra;
        private readonly ISessao _sessao;

        public UsuarioController(UsuarioRegra usuarioRegra, ISessao sessao)
        {
            _usuarioRegra = usuarioRegra;
            _sessao = sessao;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Excluir()
        {
            return View("Excluir", _sessao.BuscarSessao());
        }
        public ActionResult Editar()
        {
            return View("Editar", _sessao.BuscarSessao());
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UsuarioModel usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                _usuarioRegra.SalvarUsuario(usuario);
                } catch (Exception)
                {
                    TempData["MenssagemStatusUsuarioErro"] = $"Ocorreu um erro ao registrar sua conta.";
                    return RedirectToAction("Index", "Home");
                }
                TempData["MenssagemStatusUsuarioOk"] = $"Conta criada com sucesso.";
                return RedirectToAction("Index", "Login");
            }
            TempData["MenssagemStatusUsuarioErro"] = $"Ocorreu um erro ao registrar sua conta.";
            return RedirectToAction("Index", "Home");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UsuarioModel usuario)
        {
            usuario = _sessao.BuscarSessao();
            if (usuario != null)
            {
                _usuarioRegra.AtualizarUsuario(usuario);
                _sessao.RenovarSessao(usuario);
                TempData["MenssagemSucesso"] = $"Alteraçoes salvas com sucesso.";
                return RedirectToAction("Index", "Usuario");   
            }
            TempData["MenssagemErro"] = $"Ocorreu um erro ao salvar aletaçoes na conta.";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Excluir(UsuarioArquivoDto usuarioArquivoDto)
        {
            usuarioArquivoDto = _sessao.BuscarIdUsuarioLogado();
            if (!usuarioArquivoDto.IsNull())
            {
                _usuarioRegra.ExcluirUsuario(usuarioArquivoDto);
                _sessao.DestruirSessao();
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Excluir", "Usuario");
        }

    }
}
