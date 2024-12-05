using GestaoResiduosApi.Models;

namespace GestaoResiduosApi.Data.Repository
{
    public interface IRotaRepository
    {
        Task<RotaModel> AddAsync(RotaModel rota);
        Task<RotaModel?> GetByIdAsync(long id); // Adicionado
        Task<bool> ExistsByIdAsync(long id);
        Task DeleteByIdAsync(long id);
    }
}
