using CrudMVC.Helper;
using CrudMVC.Models;
using CrudMVC.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace CrudMVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        private readonly ISessao _sessao;

        private readonly IEmail _email;
        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao, IEmail email) {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
            _email = email;
        }
        public IActionResult Index()
        {
            //se usuario estiver logado, redirecionar para a home
            if(_sessao.BuscarSessaoUsuario() != null) return RedirectToAction("Index", "Home");

            return View();
        }

        public IActionResult Sair()
        {
            _sessao.RemoverSessaoUsuario();

            return RedirectToAction("Index","Login");
        }

        public IActionResult RedefinirSenha()
        {
            return View();
        }
        [HttpPost]
        public IActionResult EnviarLinkParaRedefinirSenha(RedefinirSenhaModel redefinirSenhaModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorEmailLogin(redefinirSenhaModel.Email, redefinirSenhaModel.Login);

                    if (usuario != null)
                    {
                        string novaSenha = usuario.GerarNovaSenha();
                        string mensagem = $"Sua nova senha é: {novaSenha}";
                        bool emailEnviado = _email.Enviar(usuario.Email, "Sistema de Contatos - nova senha", mensagem);

                        if (emailEnviado)
                        {
                            _usuarioRepositorio.Atualizar(usuario);
                            TempData["MensagemSucesso"] = "Enviamos um email para redefinir senha";
                        }
                        else
                        {
                            TempData["MensagemErro"] = "Não foi possível enviar email para redefinir senha";
                        }
                        
                        return RedirectToAction("RedefinirSenha", "Login");

                    }

                    TempData["MensagemErro"] = "Não foi possível redefinir senha, verifique os dados informados";
                }

                return View("RedefinirSenha");

            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = $"Não foi possível redefinir senha: {e.Message}";
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //procura a entidade
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorLogin(loginModel.Login);

                    if (usuario !=null)
                    {
                        //verifica se a senha da entidade é igual à informada
                        if (usuario.SenhaValida(loginModel.Senha))
                        {
                            _sessao.CriarSessaoUsuario(usuario);
                            return RedirectToAction("Index", "Home");
                        }
                        TempData["MensagemErro"] = "Senha inválida";

                    }

                    TempData["MensagemErro"] = "Usuário e/ou senha inválidos";
                }

                return View("Index");

            }
            catch (Exception e)
            {
            TempData["MensagemErro"] = $"Não foi possível fazer login: {e.Message}";
                return RedirectToAction("Index");
            }

        }
    }
}
