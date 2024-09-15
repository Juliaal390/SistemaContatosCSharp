using System.ComponentModel.DataAnnotations;

namespace CrudMVC.Models
{
    public class ContatoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email informado é inválido")]
        public string Email { get; set;}

        [Required(ErrorMessage = "O celular é obrigatório.")]
        [Phone(ErrorMessage = "O celular informado é inválido")]
        public string Celular { get; set;}
        public int? UsuarioId { get; set;}
        public UsuarioModel Usuario { get; set; }
    }
}
