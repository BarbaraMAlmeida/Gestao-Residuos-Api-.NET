using GestaoResiduosApi.Models;
using GestaoResiduosApi.Enums;

namespace GestaoResiduosApi.ViewModels
{
    public class UsuarioExibicaoViewModel
    {
        public long UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public UsuarioRole Role { get; set; }

        public UsuarioExibicaoViewModel(UsuarioModel usuario)
        {
            UsuarioId = usuario.UsuarioId;
            Nome = usuario.Nome;
            Email = usuario.Email;
            Role = usuario.Role;
        }
    }
}
