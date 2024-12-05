using GestaoResiduosApi.ViewModels;
using GestaoResiduosApi.Models;
namespace GestaoResiduosApi.Services
{
    public interface IUsuarioService
    {
        Task<UsuarioModel> AuthenticateAsync(string email, string senha);
        Task<UsuarioExibicaoViewModel> SalvarUsuario(UsuarioCadastroViewModel usuarioCadastroDto);
    }
}
