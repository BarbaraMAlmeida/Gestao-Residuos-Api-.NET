using GestaoResiduosApi.Enums;

namespace GestaoResiduosApi.ViewModels
{
    public class EmergenciaExibicaoViewModel
    {
        public DateTime DtEmergencia { get; set; }
        public StatusEmergencia Status { get; set; }
        public string Descricao { get; set; }
        public long RecipienteId { get; set; }
        public long CaminhaoId { get; set; }
    }
}
