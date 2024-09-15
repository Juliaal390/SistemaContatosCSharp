using CrudMVC.Data;
using CrudMVC.Models;

namespace CrudMVC.Repositorios
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        private readonly BancoContext _bancoContext;
        //Para atualizar o banco, é necessário injeção, que é feita no construtor da classe
        public ContatoRepositorio(BancoContext bancoContext) {
            _bancoContext = bancoContext;
            
        }
        public ContatoModel ListarPorId(int id)
        {
            return _bancoContext.Contatos.FirstOrDefault(x => x.Id == id);
            //retorna o o primeiro valor do banco que é igual ao id do parâmetro
        }

        public List<ContatoModel> BuscarTodos(int usuarioId)
        {
            return _bancoContext.Contatos.Where(x => x.UsuarioId == usuarioId).ToList();
        }
        public ContatoModel Adicionar(ContatoModel contato)
        {
            _bancoContext.Contatos.Add(contato);
            _bancoContext.SaveChanges();
            return contato;
        }

        public ContatoModel Atualizar(ContatoModel contato)
        {
            ContatoModel contatoDb = ListarPorId(contato.Id);

            if (contatoDb == null){
                throw new SystemException("Contato não existe no banco");
            }
            else
            {
                contatoDb.Nome = contato.Nome;
                contatoDb.Email = contato.Email;
                contatoDb.Celular = contato.Celular;

                _bancoContext.Contatos.Update(contatoDb);
                _bancoContext.SaveChanges();
            }
            return contatoDb;
        }

        public bool ApagarConfirmacao(int id)
        {
            ContatoModel contatoDb = ListarPorId(id);

            if (contatoDb == null)
            {
                throw new SystemException("Contato não existe no banco");
            }
            else
            {
                _bancoContext.Contatos.Remove(contatoDb);
                _bancoContext.SaveChanges();
            }
            return true;
        }
    }
}
