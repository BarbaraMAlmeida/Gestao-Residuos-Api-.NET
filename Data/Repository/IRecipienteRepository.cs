using GestaoResiduosApi.Models;

namespace GestaoResiduosApi.Data.Repository
{
    public interface IRecipienteRepository
    {
        Task<IEnumerable<RecipienteModel>> GetAllAsync();
        Task<RecipienteModel> GetByIdAsync(long id);
        Task AddAsync(RecipienteModel recipiente);
        Task SaveChangesAsync();

        Task<IEnumerable<RecipienteModel>> GetPagedAsync(int pageNumber, int pageSize);


    }
}
