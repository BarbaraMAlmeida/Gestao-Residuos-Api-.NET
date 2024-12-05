namespace GestaoResiduosApi.Models
{
    public class RotaModel
    {
        public long IdRota { get; set; }
        public DateTime DtRota { get; set; }
        public CaminhaoModel Caminhao { get; set; }
        public RecipienteModel Recipiente { get; set; }
    }
}
