using CrudMVC.Data;
using CrudMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudMVC.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly BancoContext _bancoContext;
        //Para atualizar o banco, é necessário injeção, que é feita no construtor da classe
        public UsuarioRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;

        }
        public UsuarioModel ListarPorId(int id)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x => x.Id == id);
            //retorna o o primeiro valor do banco que é igual ao id do parâmetro
        }

        public List<UsuarioModel> BuscarTodos()
        {
            //o include faz com que os usuários sejam buscados, junto com os dados de Contato Correspondentes
            return _bancoContext.Usuarios.Include(x => x.Contatos).ToList(); //lista tudo que está no banco
        }
        public UsuarioModel Adicionar(UsuarioModel usuario)
        {
            usuario.SetSenhaHash();
            usuario.DataCadastro = DateTime.Now;
            _bancoContext.Usuarios.Add(usuario);
            _bancoContext.SaveChanges();
            return usuario;
        }

        public UsuarioModel Atualizar(UsuarioModel usuario)
        {
            UsuarioModel usuarioDb = ListarPorId(usuario.Id);

            if (usuarioDb == null)
            {
                throw new SystemException("Usuário não existe no banco");
            }
            else
            {
                usuarioDb.Nome = usuario.Nome;
                usuarioDb.Email = usuario.Email;
                usuarioDb.Login = usuario.Login;
                usuarioDb.DataAtualizacao = DateTime.Now;
                usuarioDb.Perfil = usuario.Perfil;

                _bancoContext.Usuarios.Update(usuarioDb);
                _bancoContext.SaveChanges();
            }
            return usuarioDb;
        }

        public bool ApagarConfirmacao(int id)
        {
            UsuarioModel usuarioDb = ListarPorId(id);

            if (usuarioDb == null)
            {
                throw new SystemException("Usuário não existe no banco");
            }
            else
            {
                _bancoContext.Usuarios.Remove(usuarioDb);
                _bancoContext.SaveChanges();
            }
            return true;
        }

        public UsuarioModel BuscarPorLogin(string login)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper());
        }

        public UsuarioModel BuscarPorEmailLogin(string email, string login)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper() && x.Email.ToUpper() == email.ToUpper());
        }

        public UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenhaModel)
        {
            UsuarioModel usuariodb = ListarPorId(alterarSenhaModel.Id);

            if(usuariodb== null)
            {
                throw new SystemException("Erro ao atualizar senha, usuário não encontrado");
            }
            if (!usuariodb.SenhaValida(alterarSenhaModel.SenhaAtual))
            {
                    throw new SystemException("Senha atual não confere");
            }    
            if (usuariodb.SenhaValida(alterarSenhaModel.NovaSenha))
            {
                    throw new SystemException("Nova senha deve ser diferente da senha atual");
            }

            usuariodb.SetNovaSenha(alterarSenhaModel.NovaSenha);
            usuariodb.DataAtualizacao = DateTime.Now;

            _bancoContext.Usuarios.Update(usuariodb);
            _bancoContext.SaveChanges();

            return usuariodb;
        }
    }
}