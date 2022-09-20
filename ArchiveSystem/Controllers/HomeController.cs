using Microsoft.AspNetCore.Mvc;
using ArchiveSystem.LoginSessao;
using ArchiveSystem.Models;

namespace ArchiveSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISessao _sessao;

        public HomeController(ISessao sessao)
        {
            _sessao = sessao;
        }
        public IActionResult Index()
        {
            if (_sessao.BuscarSessao() != null)
            {
                return RedirectToAction("Index", "Usuario");
            }

            return View("Index");
        }

        public IActionResult Privacy()
        {
           
            return View();
        }
    }
}