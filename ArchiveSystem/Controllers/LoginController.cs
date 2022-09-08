using ArchiveSystem.Data.Repository;
using ArchiveSystem.Data.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using ArchiveSystem.Models.Entidades;
using ArchiveSystem.Models.Dtos;
using ArchiveSystem.LoginSessao;

namespace ArchiveSystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly ISessao _sessao;

        public LoginController(ISessao sessao)
        {
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            if (_sessao.BuscarSessao() != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Entrar([FromServices] UsuarioRepository repository, UsuarioLoginDto loginDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = repository.BuscarLogin(loginDto.Email);

                    if (usuario != null)
                    {
                        if (usuario.ValidaSenha(loginDto.Senha))
                        {
                            _sessao.CriarSessao(usuario);
                        return RedirectToAction("Index", "Home");
                        }
                        TempData["MenssagemErro"] = $"Email e/ou senha inválido(s).";
                    }
                    TempData["MenssagemErro"] = $"Email e/ou senha inválido(s).";
                }
                return View("Index");
            }
            catch (Exception erro)
            {

                TempData["MensssagemErro"] = $"Não foi possivel realizar seu login. Detalhe do erro: {erro.Message}";
                return RedirectToAction("Index","Login");
            }
        }

        public IActionResult Sair()
        {
            _sessao.DestruirSessao();
            return RedirectToAction("Index", "Login");
        }
    }
}
