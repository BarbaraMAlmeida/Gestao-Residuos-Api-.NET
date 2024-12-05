using GestaoResiduosApi.Enums;

namespace GestaoResiduosApi.Models
{
    public class RecipienteModel
    {
        public long IdRecipiente { get; set; }
        public long MaxCapacidade { get; set; }
        public long AtualNivel { get; set; }
        public StatusRecipiente Status { get; set; }
        public DateTime UltimaAtualizacao { get; set; }
        public long Latitude { get; set; }
        public long Longitude { get; set; }
    }
}
