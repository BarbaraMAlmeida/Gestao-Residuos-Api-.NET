using GestaoResiduosApi.Models;

namespace GestaoResiduosApi.Data.Repository
{
    public interface IRotaRepository
    {
        Task<IEnumerable<RotaModel>> GetAllAsync();
        Task<RotaModel> AddAsync(RotaModel rota);
        Task<RotaModel> GetByIdAsync(long id);
        Task<IEnumerable<RotaModel>> GetPagedAsync(int pageNumber, int pageSize);
    }
}
