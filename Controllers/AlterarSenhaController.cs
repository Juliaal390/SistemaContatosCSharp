using CrudMVC.Helper;
using CrudMVC.Models;
using CrudMVC.Repositorios;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace CrudMVC.Controllers
{
    public class AlterarSenhaController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;

        public AlterarSenhaController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Alterar(AlterarSenhaModel alterarSenhaModel)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    UsuarioModel usuarioLogado = _sessao.BuscarSessaoUsuario();
                    alterarSenhaModel.Id = usuarioLogado.Id;

                    _usuarioRepositorio.AlterarSenha(alterarSenhaModel);

                    TempData["Sucesso"] = "Senha alterada com sucesso";
                    return View("Index", alterarSenhaModel);

                }
                TempData["Erro"] = $"Não foi possível alterar senha";
                return View("Index", alterarSenhaModel);

            }catch(Exception ex)
            {
                TempData["Erro"] = $"Não foi possível alterar senha: {ex.Message}";
                return View("Index", alterarSenhaModel);
            }
        }
    }
}
