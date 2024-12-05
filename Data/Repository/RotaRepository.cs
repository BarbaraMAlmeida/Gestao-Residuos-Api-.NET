using GestaoResiduosApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GestaoResiduosApi.Data.Repository
{
    public class RotaRepository : IRotaRepository
    {
        private readonly DatabaseContext _context;

        public RotaRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<RotaModel> AddAsync(RotaModel rota)
        {
            var entity = await _context.Rota.AddAsync(rota);
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<RotaModel?> GetByIdAsync(long id) // Implementação do método
        {
            return await _context.Rota.FirstOrDefaultAsync(r => r.IdRota == id);
        }

        public async Task<bool> ExistsByIdAsync(long id)
        {
            return await _context.Rota.AnyAsync(r => r.IdRota == id);
        }

        public async Task DeleteByIdAsync(long id)
        {
            var rota = await _context.Rota.FirstOrDefaultAsync(r => r.IdRota == id);
            if (rota != null)
            {
                _context.Rota.Remove(rota);
                await _context.SaveChangesAsync();
            }
        }
    }
}