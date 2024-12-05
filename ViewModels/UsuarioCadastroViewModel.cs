using GestaoResiduosApi.Models;
using GestaoResiduosApi.Enums;
using System.ComponentModel.DataAnnotations;

namespace GestaoResiduosApi.ViewModels
{
    public class UsuarioCadastroViewModel
    {
        public long? UsuarioId { get; set; }

        [Required(ErrorMessage = "O campo nome é obrigatório!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "E-mail é obrigatório!")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória!")]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 10 caracteres")]
        public string Senha { get; set; }

        public UsuarioRole Role { get; set; }
    }
}
