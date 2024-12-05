using GestaoResiduosApi.ViewModels;

namespace GestaoResiduosApi.Services
{
    public interface IRecipienteService
    {
        Task<IEnumerable<RecipienteExibicaoViewModel>> GetAllAsync();
        Task AddAsync(RecipienteCadastroViewModel viewModel);
        Task<IEnumerable<RecipienteExibicaoViewModel>> GetPagedAsync(int pageNumber, int pageSize);
    }
}
