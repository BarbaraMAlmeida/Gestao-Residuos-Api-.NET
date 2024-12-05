using GestaoResiduosApi.ViewModels;

namespace GestaoResiduosApi.Services
{
    public interface IEmergenciaService
    {
        Task<IEnumerable<EmergenciaExibicaoViewModel>> GetAllAsync();
        Task<EmergenciaExibicaoViewModel> AddAsync(EmergenciaCadastroViewModel model);
    }
}
