using ArchiveSystem.Data.Repository;
using ArchiveSystem.Data.UnitOfWork;
using ArchiveSystem.Models.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArchiveSystem.Controllers
{
    public class UsuarioController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Details(int? id)
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Excluir([FromServices] UsuarioRepository repository, int id)
        {
            UsuarioModel usuario = repository.FindOne(id).FirstOrDefault();
            return View(usuario);
        }
        public ActionResult Editar([FromServices] UsuarioRepository repository, int id)
        {
            UsuarioModel usuario = repository.FindOne(id).FirstOrDefault();
            return View("Editar", usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromServices] IUnitOfWork uow, [FromServices] UsuarioRepository repository, UsuarioModel usuario)
        {
            uow.BeginTransaction();
            repository.Create(usuario);
            uow.Commit();
            return RedirectToAction("Index", "Login");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([FromServices] IUnitOfWork uow, [FromServices] UsuarioRepository repository, UsuarioModel usuario)
        {
            uow.BeginTransaction(); 
            repository.Edit(usuario);
            uow.Commit();
            return RedirectToAction("Index", "Home");        
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Excluir([FromServices] IUnitOfWork uow, [FromServices] UsuarioRepository repository, int id)
        {
            uow.BeginTransaction();
            repository.Excluir(id);
            uow.Commit();
            return RedirectToAction("Index", "Usuario");
        }

    }
}
