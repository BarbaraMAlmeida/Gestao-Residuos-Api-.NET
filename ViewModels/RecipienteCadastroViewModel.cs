using GestaoResiduosApi.Enums;
using System.ComponentModel.DataAnnotations;

namespace GestaoResiduosApi.ViewModels
{
    public class RecipienteCadastroViewModel
    {
        [Required(ErrorMessage = "A capacidade máxima é obrigatória.")]
        public long? MaxCapacidade { get; set; }

        [Required(ErrorMessage = "O nível atual é obrigatório.")]
        public long? AtualNivel { get; set; }

        [Required(ErrorMessage = "O status é obrigatório.")]
        public StatusRecipiente? Status { get; set; }

        [Required(ErrorMessage = "A data de atualização é obrigatória.")]
        public DateTime? UltimaAtualizacao { get; set; }

        [Required(ErrorMessage = "A Latitude é obrigatória.")]
        public long? Latitude { get; set; }

        [Required(ErrorMessage = "A Longitude é obrigatória.")]
        public long? Longitude { get; set; }
    }

}
