using CrudMVC.Enums;
using CrudMVC.Helper;
using System.ComponentModel.DataAnnotations;

namespace CrudMVC.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string Login { get; set;}


        [Required(ErrorMessage = "O Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email informado é inválido")]
        public string Email { get; set;}


        [Required(ErrorMessage = "O perfil é obrigatório.")]
        public PerfilEnum Perfil { get; set;}


        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string Senha { get; set;}

        public DateTime DataCadastro { get; set;}
        public DateTime? DataAtualizacao { get; set;} //campo pode ser nulo

        public bool SenhaValida(string senha)
        {
            return Senha == senha.GerarHash(); //verifica se senha da model é igual à senha passada por parametro
        }

        //ao esquecer senha
        public void SetSenhaHash()
        {
            Senha = Senha.GerarHash(); //atualiza a senha atual com hash
        }

        public string GerarNovaSenha()
        {
            string novaSenha = Guid.NewGuid().ToString().Substring(0, 8);
            Senha = novaSenha.GerarHash();
            return novaSenha;
        }

        //ao atualizar senha
        public void SetNovaSenha(string novaSenha)
        {
            Senha = novaSenha.GerarHash(); //atualiza a senha atual com hash
        }

        public virtual List<ContatoModel> Contatos { get; set; }
    }
}
