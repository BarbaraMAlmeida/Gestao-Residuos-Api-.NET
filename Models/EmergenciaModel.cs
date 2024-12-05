using GestaoResiduosApi.Enums;

namespace GestaoResiduosApi.Models
{
    public class EmergenciaModel
    {
        public long IdEmergencia { get; set; }
        public DateTime DtEmergencia { get; set; }
        public StatusEmergencia Status { get; set; }
        public string Descricao { get; set; }
        public RecipienteModel Recipiente { get; set; }
        public CaminhaoModel Caminhao { get; set; }
    }
}
