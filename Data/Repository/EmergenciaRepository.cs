using GestaoResiduosApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GestaoResiduosApi.Data.Repository
{
    public class EmergenciaRepository : IEmergenciaRepository
    {
        private readonly DatabaseContext _context;

        public EmergenciaRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmergenciaModel>> GetAllAsync()
        {
            return await _context.Emergencia
                .Include(e => e.Recipiente)
                .Include(e => e.Caminhao)
                .ToListAsync();
        }

        public async Task<EmergenciaModel> AddAsync(EmergenciaModel emergencia)
        {
            var entity = await _context.Emergencia.AddAsync(emergencia);
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<RecipienteModel> GetRecipienteByIdAsync(long id)
        {
            Console.WriteLine($"Buscando Recipiente com Id: {id}");
            return await _context.Recipiente.FirstOrDefaultAsync(r => r.IdRecipiente == id);
        }

        public async Task<CaminhaoModel> GetCaminhaoByIdAsync(long id)
        {
            Console.WriteLine($"Buscando Caminhão com Id: {id}");
            return await _context.Caminhoes.FirstOrDefaultAsync(c => c.IdCaminhao == id);
        }

        public async Task<IEnumerable<EmergenciaModel>> GetPagedAsync(int pageNumber, int pageSize)
{
    return await _context.Emergencia
        .Include(e => e.Recipiente) // Carrega Recipiente
        .Include(e => e.Caminhao)   // Carrega Caminhão
        .OrderBy(e => e.IdEmergencia)
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();
    }
    }
}
