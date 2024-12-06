using GestaoResiduosApi.ViewModels;

namespace GestaoResiduosApi.Services
{
    public interface IAgendamentoService
    {
        Task<IEnumerable<AgendamentoExibicaoViewModel>> GetAllAsync();
        Task<IEnumerable<AgendamentoExibicaoViewModel>> GetPagedAsync(int pageNumber, int pageSize);
        Task<AgendamentoExibicaoViewModel> CreateAsync(AgendamentoCadastroViewModel model);
        Task<AgendamentoExibicaoViewModel> UpdateAsync(AgendamentoCadastroViewModel model, long id);
        Task<bool> DeleteAsync(long id);
    }
}