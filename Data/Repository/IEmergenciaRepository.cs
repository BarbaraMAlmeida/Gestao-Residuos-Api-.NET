using GestaoResiduosApi.Models;

namespace GestaoResiduosApi.Data.Repository
{
    public interface IEmergenciaRepository
    {
        Task<IEnumerable<EmergenciaModel>> GetAllAsync();
        Task<EmergenciaModel> AddAsync(EmergenciaModel emergencia);
        Task<RecipienteModel> GetRecipienteByIdAsync(long id);
        Task<CaminhaoModel> GetCaminhaoByIdAsync(long id);
    }
}
