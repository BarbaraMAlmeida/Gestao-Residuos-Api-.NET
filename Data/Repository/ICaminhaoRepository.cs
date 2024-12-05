using GestaoResiduosApi.Models;

namespace GestaoResiduosApi.Data.Repository
{
    public interface ICaminhaoRepository
    {
        Task<IEnumerable<CaminhaoModel>> GetAllAsync();
        Task<CaminhaoModel> AddAsync(CaminhaoModel caminhao);
        Task<CaminhaoModel?> GetByIdAsync(long id); // Adicionado
    }
}
