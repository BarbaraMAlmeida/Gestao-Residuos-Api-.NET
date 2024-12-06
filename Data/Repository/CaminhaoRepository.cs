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

        public async Task<CaminhaoModel?> GetByIdAsync(long id)
        {
            return await _context.Caminhoes.FindAsync(id);
        }

        public async Task AddAsync(CaminhaoModel caminhao)
        {
            await _context.Caminhoes.AddAsync(caminhao);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CaminhaoModel>> GetPagedAsync(int pageNumber, int pageSize)
        {
            return await _context.Caminhoes
                .OrderBy(r => r.IdCaminhao) // Ordenação para garantir consistência
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }


    }
}
