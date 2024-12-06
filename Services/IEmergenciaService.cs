using GestaoResiduosApi.ViewModels;

namespace GestaoResiduosApi.Services
{
    public interface IEmergenciaService
    {
        Task<IEnumerable<EmergenciaExibicaoViewModel>> GetAllAsync();
        Task<EmergenciaExibicaoViewModel> AddAsync(EmergenciaCadastroViewModel model);
        Task<IEnumerable<EmergenciaExibicaoViewModel>> GetPagedAsync(int pageNumber, int pageSize);
    }
}
