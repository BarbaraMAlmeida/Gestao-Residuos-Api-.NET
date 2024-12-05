using GestaoResiduosApi.Enums;

namespace GestaoResiduosApi.ViewModels
{
    public class AgendamentoExibicaoViewModel
    {
        public long IdAgendamento { get; set; }
        public DateTime DtAgendamento { get; set; }
        public StatusAgendamento StatusAgendamento { get; set; }
        public long RotaId { get; set; }
    }
}
