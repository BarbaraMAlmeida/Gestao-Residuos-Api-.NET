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

        public async Task<IEnumerable<RotaModel>> GetAllAsync()
        {
            return await _context.Rota
                .Include(a => a.Recipiente)
                .Include(a => a.Caminhao)
                .ToListAsync();
        }

        public async Task<RotaModel> AddAsync(RotaModel rota)
        {
            var entity = await _context.Rota.AddAsync(rota);
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<RotaModel> GetByIdAsync(long id)
        {
            Console.WriteLine("ID:" + id);
            return await _context.Rota
                .Include(a => a.Recipiente)
                .Include(a => a.Caminhao)
                .FirstOrDefaultAsync(a => a.IdRota == id);
        }


        public async Task<IEnumerable<RotaModel>> GetPagedAsync(int pageNumber, int pageSize)
        {
            return await _context.Rota
                .Include(e => e.Recipiente) // Carrega Recipiente
                .Include(e => e.Caminhao)   // Carrega Caminhão
                .OrderBy(e => e.IdRota)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

    }
}