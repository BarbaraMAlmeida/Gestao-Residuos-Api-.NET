using System.ComponentModel.DataAnnotations;

namespace GestaoResiduosApi.ViewModels
{
    public class CaminhaoCadastroViewModel
    {
        [Required(ErrorMessage = "A Placa é obrigatória.")]
        public string?  Placa { get; set; }

        [Required(ErrorMessage = "A Capacidade é obrigatória.")]
        public long Capacidade { get; set; }
    }
}
