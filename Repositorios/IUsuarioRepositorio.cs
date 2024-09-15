using CrudMVC.Models;

namespace CrudMVC.Repositorios
{
    public interface IUsuarioRepositorio
    {
        UsuarioModel BuscarPorLogin(string login);
        UsuarioModel BuscarPorEmailLogin(string email, string login);
        UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenhaModel);
        UsuarioModel ListarPorId(int id);
        List<UsuarioModel> BuscarTodos();
        UsuarioModel Adicionar(UsuarioModel usuario);

        UsuarioModel Atualizar(UsuarioModel usuario);
        bool ApagarConfirmacao(int id);
    }
}
