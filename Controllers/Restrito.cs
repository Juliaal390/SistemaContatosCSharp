using CrudMVC.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CrudMVC.Controllers
{
    [PaginaParaUsuarioLogado]
    public class Restrito : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
