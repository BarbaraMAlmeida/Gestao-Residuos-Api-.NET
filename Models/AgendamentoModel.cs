using GestaoResiduosApi.Enums;

namespace GestaoResiduosApi.Models
{
    public class AgendamentoModel
    {
        public long IdAgendamento { get; set; }
        public DateTime DtAgendamento { get; set; }
        public StatusAgendamento StatusAgendamento { get; set; }
        public RotaModel Rota { get; set; }
    }
}
