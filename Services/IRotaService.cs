using GestaoResiduosApi.ViewModels;
namespace GestaoResiduosApi.Services
{
    public interface IRotaService
    {
        Task<IEnumerable<RotaExibicaoViewModel>> GetAllAsync();
        Task<IEnumerable<RotaExibicaoViewModel>> GetPagedAsync(int pageNumber, int pageSize);
        Task<RotaExibicaoViewModel> CreateAsync(RotaCadastroViewModel model);
    }
}
