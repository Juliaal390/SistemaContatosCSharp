using CrudMVC.Filters;
using CrudMVC.Models;
using CrudMVC.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace CrudMVC.Controllers
{
    [PaginaRestritaSomenteAdmin]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        //construtor
        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio; //injeção
        }
        public IActionResult Index()
        {
            List<UsuarioModel> usuarios = _usuarioRepositorio.BuscarTodos();
            return View(usuarios);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Apagar(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id);
            return View(usuario);
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            try
            {
                TempData["Sucesso"] = "Usuário apagado com sucesso";
                _usuarioRepositorio.ApagarConfirmacao(id);
                return RedirectToAction("Index");
            }
            catch (System.Exception erro)
            {
                TempData["Erro"] = $"Não foi possível apagar usuário: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
        public IActionResult Editar(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id);
            if (usuario == null)
            {
                // Tratar o caso onde o usuário não é encontrado
                TempData["Erro"] = "Usuário não encontrado.";
                return RedirectToAction("Index");
            }

            return View(usuario); // Passa o modelo para a View
        }


        [HttpPost]
        public IActionResult Alterar(UsuarioSemSenhaModel usuarioSemSenha)
        {
            try
            {
                UsuarioModel usuario = null;
                if (ModelState.IsValid)
                {
                    usuario = new UsuarioModel()
                    {
                        Id = usuarioSemSenha.Id,
                        Nome = usuarioSemSenha.Nome,
                        Login = usuarioSemSenha.Login,
                        Email = usuarioSemSenha.Email,
                        Perfil = usuarioSemSenha.Perfil
                    };

                    TempData["Sucesso"] = "Usuario alterado com sucesso";
                    _usuarioRepositorio.Atualizar(usuario);
                    return RedirectToAction("Index");
                }

                return View("Editar", usuario); //retorna a view Editar, passando os dados de contato
            }
            catch (System.Exception erro)
            {
                TempData["Erro"] = $"Erro ao alterar usuário: {erro.Message}";
                return RedirectToAction("Index");
                throw;
            }
        }

        [HttpPost]
        public IActionResult Criar(UsuarioModel usuario)
        {
            try
            {
                if (ModelState.IsValid) //se a informação da model for válida
                {
                    TempData["Sucesso"] = "Usuário cadastrado com sucesso";
                    _usuarioRepositorio.Adicionar(usuario);
                    return RedirectToAction("Index");
                }

                return View(usuario); //else
            }
            catch (System.Exception erro)
            {
                TempData["Erro"] = $"Não foi possível cadastrar usuário: {erro.Message}";
                return RedirectToAction("Index");
                throw;
            }
        }
    }
}
