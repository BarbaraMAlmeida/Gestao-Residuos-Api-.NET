using GestaoResiduosApi.ViewModels;

namespace GestaoResiduosApi.Services
{
    public interface ICaminhaoService
    {
        Task<IEnumerable<CaminhaoExibicaoViewModel>> GetAllAsync();
        Task AddAsync(CaminhaoCadastroViewModel viewModel);
        Task<IEnumerable<CaminhaoExibicaoViewModel>> GetPagedAsync(int pageNumber, int pageSize);
    }
}
