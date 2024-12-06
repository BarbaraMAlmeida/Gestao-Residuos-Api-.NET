using GestaoResiduosApi.Enums;
using System.ComponentModel.DataAnnotations;

namespace GestaoResiduosApi.ViewModels
{
    public class EmergenciaCadastroViewModel
    {
        [Required(ErrorMessage = "A Data é obrigatória.")]
        public DateTime DtEmergencia { get; set; }
        [Required(ErrorMessage = "O Status é obrigatório.")]
        public StatusEmergencia Status { get; set; }
        [Required(ErrorMessage = "A descricao é obrigatória.")]
        public string? Descricao { get; set; }
        [Required(ErrorMessage = "idRecipiente obrigatório.")]
        public long RecipienteId { get; set; }
        [Required(ErrorMessage = "idCaminhao obrigatório.")]
        public long CaminhaoId { get; set; }
    }
}
