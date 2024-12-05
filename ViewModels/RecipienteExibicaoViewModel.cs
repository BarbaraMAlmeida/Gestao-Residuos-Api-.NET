using GestaoResiduosApi.Enums;

namespace GestaoResiduosApi.ViewModels
{
    public class RecipienteExibicaoViewModel
    {
        public long MaxCapacidade { get; set; }
        public long AtualNivel { get; set; }
        public StatusRecipiente Status { get; set; }
        public DateTime UltimaAtualizacao { get; set; }
        public long Latitude { get; set; }
        public long Longitude { get; set; }
    }
}
