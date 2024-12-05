using GestaoResiduosApi.Data.Repository;
using GestaoResiduosApi.Models;
using GestaoResiduosApi.ViewModels;
namespace GestaoResiduosApi.Services
{
    public class AgendamentoService : IAgendamentoService
    {
        private readonly IAgendamentoRepository _agendamentoRepository;
        private readonly IRotaRepository _rotaRepository;

        public AgendamentoService(IAgendamentoRepository agendamentoRepository, IRotaRepository rotaRepository)
        {
            _agendamentoRepository = agendamentoRepository;
            _rotaRepository = rotaRepository;
        }

        public async Task<IEnumerable<AgendamentoExibicaoViewModel>> GetAllAsync()
        {
            var agendamentos = await _agendamentoRepository.GetAllAsync();
            return agendamentos.Select(a => new AgendamentoExibicaoViewModel
            {
                IdAgendamento = a.IdAgendamento,
                DtAgendamento = a.DtAgendamento,
                StatusAgendamento = a.StatusAgendamento,
                RotaId = a.Rota?.IdRota ?? 0
            });
        }

        public async Task<AgendamentoExibicaoViewModel> CreateAsync(AgendamentoCadastroViewModel model)
        {
            var rota = await _rotaRepository.GetByIdAsync(model.RotaId);
            if (rota == null) throw new KeyNotFoundException("Rota não encontrada.");

            var agendamento = new AgendamentoModel
            {
                DtAgendamento = model.DtAgendamento,
                StatusAgendamento = model.StatusAgendamento,
                Rota = rota
            };

            var savedAgendamento = await _agendamentoRepository.AddAsync(agendamento);

            return new AgendamentoExibicaoViewModel
            {
                IdAgendamento = savedAgendamento.IdAgendamento,
                DtAgendamento = savedAgendamento.DtAgendamento,
                StatusAgendamento = savedAgendamento.StatusAgendamento,
                RotaId = savedAgendamento.Rota.IdRota
            };
        }

        public async Task<AgendamentoExibicaoViewModel> UpdateAsync(AgendamentoCadastroViewModel model, long id)
        {
            var agendamento = await _agendamentoRepository.GetByIdAsync(id);
            if (agendamento == null) throw new KeyNotFoundException("Agendamento não encontrado.");

            var rota = await _rotaRepository.GetByIdAsync(model.RotaId);
            if (rota == null) throw new KeyNotFoundException("Rota não encontrada.");

            agendamento.DtAgendamento = model.DtAgendamento;
            agendamento.StatusAgendamento = model.StatusAgendamento;
            agendamento.Rota = rota;

            var updatedAgendamento = await _agendamentoRepository.UpdateAsync(agendamento);

            return new AgendamentoExibicaoViewModel
            {
                IdAgendamento = updatedAgendamento.IdAgendamento,
                DtAgendamento = updatedAgendamento.DtAgendamento,
                StatusAgendamento = updatedAgendamento.StatusAgendamento,
                RotaId = updatedAgendamento.Rota.IdRota
            };
        }

        public async Task<bool> DeleteAsync(long id)
        {
            return await _agendamentoRepository.DeleteByIdAsync(id);
        }
    }
}
