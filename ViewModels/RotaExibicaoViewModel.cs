namespace GestaoResiduosApi.ViewModels
{
    public class RotaExibicaoViewModel
    {
        public long IdRota { get; set; }
        public DateTime DtRota { get; set; }
        public long RecipienteId { get; set; }
        public long CaminhaoId { get; set; }
    }
}
