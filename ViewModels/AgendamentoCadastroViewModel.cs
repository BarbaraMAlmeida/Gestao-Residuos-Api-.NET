using GestaoResiduosApi.Enums;
using System.ComponentModel.DataAnnotations;

namespace GestaoResiduosApi.ViewModels
{
    public class AgendamentoCadastroViewModel
    {
        [Required(ErrorMessage = "A data de agendamento é obrigatória.")]
        public DateTime DtAgendamento { get; set; }

        [Required(ErrorMessage = "O status do agendamento é obrigatório.")]
        public StatusAgendamento StatusAgendamento { get; set; }

        [Required(ErrorMessage = "O ID da rota é obrigatório.")]
        public long RotaId { get; set; }
    }
}
