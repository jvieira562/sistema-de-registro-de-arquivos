using Microsoft.AspNetCore.Mvc;
using ArchiveSystem.LoginSessao;
using ArchiveSystem.Dtos.Usuario;
using ArchiveSystem.Models.Entidades;
using ArchiveSystem.Domain.Regras.Usuario;

namespace ArchiveSystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly ISessao _sessao;
        private readonly UsuarioRegra _usuarioRegra;

        public LoginController(ISessao sessao, UsuarioRegra usuarioRegra)
        {
            _usuarioRegra = usuarioRegra;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            if (_sessao.BuscarSessao() != null)
            {
                return RedirectToAction("Index", "Usuario");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Entrar(UsuarioLoginDto loginDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_usuarioRegra.ValidaLogin(loginDto))
                    {
                        UsuarioModel usuario = _usuarioRegra.BuscarUsuario(loginDto.Email);
                        _sessao.CriarSessao(usuario);
                          return RedirectToAction("Index", "Usuario");
                    }

                    TempData["MenssagemStatusLogin"] = $"Email e/ou senha inválido(s).";

                }
                return View("Index");
            }
            catch (Exception erro)
            {

                TempData["MenssagemStatusLogin"] = $"Não foi possivel realizar seu login. Detalhe do erro: {erro.Message}";
                return RedirectToAction("Index", "Login");
            }
        }
        public IActionResult Sair()
        {
            _sessao.DestruirSessao();
            return RedirectToAction("Index", "Login");
        }
    }
}
