using GestaoResiduosApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GestaoResiduosApi.Data.Repository
{
    public class AgendamentoRepository : IAgendamentoRepository
    {
        private readonly DatabaseContext _context;

        public AgendamentoRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AgendamentoModel>> GetAllAsync()
        {
            return await _context.Agendamento
                .Include(a => a.Rota)
                .ToListAsync();
        }

        public async Task<AgendamentoModel> AddAsync(AgendamentoModel agendamento)
        {
            var entity = await _context.Agendamento.AddAsync(agendamento);
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<AgendamentoModel> UpdateAsync(AgendamentoModel agendamento)
        {
            _context.Agendamento.Update(agendamento);
            await _context.SaveChangesAsync();
            return agendamento;
        }

        public async Task<AgendamentoModel> GetByIdAsync(long id)
        {
            Console.WriteLine("ID:" + id);
            return await _context.Agendamento
                .Include(a => a.Rota)
                .FirstOrDefaultAsync(a => a.Rota.IdRota == id);
        }

        public async Task<bool> DeleteByIdAsync(long id)
        {
            var agendamento = await _context.Agendamento.FindAsync(id);
            if (agendamento == null) return false;

            _context.Agendamento.Remove(agendamento);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<AgendamentoModel>> GetPagedAsync(int pageNumber, int pageSize)
        {
            return await _context.Agendamento
                .Include(e => e.Rota) // Carrega Rota
                .OrderBy(e => e.IdAgendamento)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
