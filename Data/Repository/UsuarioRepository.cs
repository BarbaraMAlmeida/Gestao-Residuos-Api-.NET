using GestaoResiduosApi.Models;
using Microsoft.EntityFrameworkCore;
namespace GestaoResiduosApi.Data.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DatabaseContext _context;

        public UsuarioRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<UsuarioModel> FindByEmailAsync(string email)
        {
            // Corrigido: Usa FirstOrDefaultAsync corretamente
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<UsuarioModel> SaveAsync(UsuarioModel usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }
    }
}
