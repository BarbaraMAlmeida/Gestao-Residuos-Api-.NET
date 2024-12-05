using GestaoResiduosApi.Models;
namespace GestaoResiduosApi.Data.Repository
{
    public interface IUsuarioRepository
    {
        Task<UsuarioModel> FindByEmailAsync(string email);
        Task<UsuarioModel> SaveAsync(UsuarioModel usuario);
    }
}
