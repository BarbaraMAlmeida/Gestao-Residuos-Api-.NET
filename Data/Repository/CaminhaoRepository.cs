using GestaoResiduosApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GestaoResiduosApi.Data.Repository
{
    public class CaminhaoRepository : ICaminhaoRepository
    {
        private readonly DatabaseContext _context;

        public CaminhaoRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CaminhaoModel>> GetAllAsync()
        {
            return await _context.Caminhoes.ToListAsync();
        }

        public async Task<CaminhaoModel> AddAsync(CaminhaoModel caminhao)
        {
            var entity = await _context.Caminhoes.AddAsync(caminhao);
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<CaminhaoModel?> GetByIdAsync(long id) // Implementação
        {
            return await _context.Caminhoes.FirstOrDefaultAsync(c => c.IdCaminhao == id);
        }
    }
}
