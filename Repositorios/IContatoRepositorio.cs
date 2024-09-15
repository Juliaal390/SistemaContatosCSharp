using CrudMVC.Models;

namespace CrudMVC.Repositorios
{
    public interface IContatoRepositorio
    {
        ContatoModel ListarPorId(int id);
        List<ContatoModel> BuscarTodos(int usuarioId);
        ContatoModel Adicionar(ContatoModel contato);

        ContatoModel Atualizar(ContatoModel contato);
        bool ApagarConfirmacao(int id);
    }
}
