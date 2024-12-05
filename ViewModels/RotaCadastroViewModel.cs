using System.ComponentModel.DataAnnotations;

namespace GestaoResiduosApi.ViewModels
{
    public class RotaCadastroViewModel
    {
        [Required(ErrorMessage = "A data da rota é obrigatória.")]
        public DateTime DtRota { get; set; }

        [Required(ErrorMessage = "O ID do recipiente é obrigatório.")]
        public long RecipienteId { get; set; }

        [Required(ErrorMessage = "O ID do caminhão é obrigatório.")]
        public long CaminhaoId { get; set; }
    }
}
