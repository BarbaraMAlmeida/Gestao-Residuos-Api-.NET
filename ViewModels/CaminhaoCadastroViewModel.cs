using System.ComponentModel.DataAnnotations;

namespace GestaoResiduosApi.ViewModels
{
    public class CaminhaoCadastroViewModel
    {
        [Required]
        public string Placa { get; set; }
        [Required]
        public long Capacidade { get; set; }
    }
}
