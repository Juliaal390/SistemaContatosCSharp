using CrudMVC.Enums;
using System.ComponentModel.DataAnnotations;

namespace CrudMVC.Models
{
    public class UsuarioSemSenhaModel
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

    }
}
