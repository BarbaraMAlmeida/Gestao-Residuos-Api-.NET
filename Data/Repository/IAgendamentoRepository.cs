using GestaoResiduosApi.Models;

namespace GestaoResiduosApi.Data.Repository
{
    public interface IAgendamentoRepository
    {
        Task<IEnumerable<AgendamentoModel>> GetAllAsync();
        Task<AgendamentoModel> AddAsync(AgendamentoModel agendamento);
        Task<AgendamentoModel> UpdateAsync(AgendamentoModel agendamento);
        Task<AgendamentoModel> GetByIdAsync(long id);
        Task<bool> DeleteByIdAsync(long id);
        Task<IEnumerable<AgendamentoModel>> GetPagedAsync(int pageNumber, int pageSize);
    }
}
