using GestaoResiduosApi.ViewModels;
namespace GestaoResiduosApi.Services
{
    public interface IRotaService
    {
        Task<RotaExibicaoViewModel> CriarRotaAsync(RotaCadastroViewModel model);
        Task DeletarRotaAsync(long id);
    }
}
