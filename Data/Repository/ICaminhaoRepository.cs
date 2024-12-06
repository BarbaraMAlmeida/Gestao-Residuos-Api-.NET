using GestaoResiduosApi.Models;

namespace GestaoResiduosApi.Data.Repository
{
    public interface ICaminhaoRepository
    {
        Task<IEnumerable<CaminhaoModel>> GetAllAsync();
        Task<CaminhaoModel> GetByIdAsync(long id);
        Task AddAsync(CaminhaoModel recipiente);
        Task SaveChangesAsync();

        Task<IEnumerable<CaminhaoModel>> GetPagedAsync(int pageNumber, int pageSize);
    }
}
