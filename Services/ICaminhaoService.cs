using GestaoResiduosApi.ViewModels;

namespace GestaoResiduosApi.Services
{
    public interface ICaminhaoService
    {
        Task<IEnumerable<CaminhaoExibicaoViewModel>> GetAllAsync();
        Task<CaminhaoExibicaoViewModel> AddAsync(CaminhaoCadastroViewModel model);
    }
}
