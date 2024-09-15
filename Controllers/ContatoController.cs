using Microsoft.AspNetCore.Mvc;
using CrudMVC.Models;
using CrudMVC.Repositorios;
using CrudMVC.Filters;
using CrudMVC.Helper; //é preciso importar para usar "contatoModel contato", pois contatoModel está em outro namespace

namespace CrudMVC.Controllers
{
    [PaginaParaUsuarioLogado]
    public class ContatoController : Controller
    {
        private readonly IContatoRepositorio _contatoRepositorio;
        private readonly ISessao _sessao;
        //construtor
        public ContatoController(IContatoRepositorio contatoRepositorio, ISessao sessao) {
            _contatoRepositorio = contatoRepositorio; //injeção
            _sessao = sessao;

        }

        public IActionResult Index()
        {
            UsuarioModel usuarioLogado = _sessao.BuscarSessaoUsuario();

            List<ContatoModel> contatos = _contatoRepositorio.BuscarTodos(usuarioLogado.Id);
            return View(contatos);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar(int id)
        {
            ContatoModel contato = _contatoRepositorio.ListarPorId(id);
            return View(contato); //retorna a view com os dados correspondentes ao id enviado
        }

        public IActionResult Apagar(int id)
        {
            ContatoModel contato = _contatoRepositorio.ListarPorId(id);
            return View(contato);
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            try
            {
                TempData["Sucesso"] = "Contato apagado com sucesso";
                _contatoRepositorio.ApagarConfirmacao(id);
                return RedirectToAction("Index");
            }
            catch (System.Exception erro)
            {
                TempData["Erro"] = $"Não foi possível apagar contato: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Criar(ContatoModel contato)
        {
            try {
                if (ModelState.IsValid) //se a informação da model for válida
                {
                    UsuarioModel usuarioLogado = _sessao.BuscarSessaoUsuario();
                    contato.UsuarioId = usuarioLogado.Id;

                    TempData["Sucesso"] = "Contato cadastrado com sucesso";
                    _contatoRepositorio.Adicionar(contato);
                    return RedirectToAction("Index");
                }

                return View(contato); //else
            }
            catch(System.Exception erro)
            {
                TempData["Erro"] = $"Não foi possível cadastrar contato: {erro.Message}";
                return RedirectToAction("Index");
                throw;
            }
        }

        [HttpPost]
        public IActionResult Alterar(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuarioLogado = _sessao.BuscarSessaoUsuario();
                    contato.UsuarioId = usuarioLogado.Id;

                    TempData["Sucesso"] = "Contato alterado com sucesso";
                    _contatoRepositorio.Atualizar(contato);
                    return RedirectToAction("Index");
                }

                return View("Editar", contato); //retorna a view Editar, passando os dados de contato
            }
            catch(System.Exception erro)
            {
                TempData["Erro"] = $"Erro ao alterar cadastro: {erro.Message}";
                return RedirectToAction("Index");
                throw;
            }
        }
    }
}
