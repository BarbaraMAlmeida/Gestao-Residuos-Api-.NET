using GestaoResiduosApi.Data;
using GestaoResiduosApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GestaoResiduosApi.Data.Repository
{
    public class RecipienteRepository : IRecipienteRepository
    {
        private readonly DatabaseContext _context;

        public RecipienteRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RecipienteModel>> GetAllAsync()
        {
            return await _context.Recipiente.ToListAsync();
        }

        public async Task<RecipienteModel> GetByIdAsync(long id)
        {
            return await _context.Recipiente.FindAsync(id);
        }

        public async Task AddAsync(RecipienteModel recipiente)
        {
            await _context.Recipiente.AddAsync(recipiente);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RecipienteModel>> GetPagedAsync(int pageNumber, int pageSize)
        {
            return await _context.Recipiente
                .OrderBy(r => r.IdRecipiente) // Ordenação para garantir consistência
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
