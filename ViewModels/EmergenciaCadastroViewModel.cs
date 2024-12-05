using GestaoResiduosApi.Enums;
using System.ComponentModel.DataAnnotations;

namespace GestaoResiduosApi.ViewModels
{
    public class EmergenciaCadastroViewModel
    {
        [Required]
        public DateTime DtEmergencia { get; set; }
        [Required]
        public StatusEmergencia Status { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        public long RecipienteId { get; set; }
        [Required]
        public long CaminhaoId { get; set; }
    }
}
