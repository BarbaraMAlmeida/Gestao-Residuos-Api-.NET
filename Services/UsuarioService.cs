using GestaoResiduosApi.Data.Repository;
using GestaoResiduosApi.ViewModels;
using GestaoResiduosApi.Models;
using Microsoft.AspNetCore.Identity;
using AutoMapper;

namespace GestaoResiduosApi.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPasswordHasher<UsuarioModel> _passwordHasher;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository usuarioRepository, IPasswordHasher<UsuarioModel> passwordHasher, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }

        public async Task<UsuarioModel> AuthenticateAsync(string email, string senha)
        {
            var usuario = await _usuarioRepository.FindByEmailAsync(email);

            if (usuario == null || _passwordHasher.VerifyHashedPassword(usuario, usuario.Senha, senha) == PasswordVerificationResult.Failed)
            {
                return null;
            }

            return usuario;
        }

        public async Task<UsuarioExibicaoViewModel> SalvarUsuario(UsuarioCadastroViewModel usuarioCadastroDto)
        {
            // Mapeia o ViewModel para o Model
            var usuario = _mapper.Map<UsuarioModel>(usuarioCadastroDto);

            // Hash da senha antes de salvar
            usuario.Senha = _passwordHasher.HashPassword(usuario, usuarioCadastroDto.Senha);

            // Salva o usuário no banco de dados
            var usuarioSalvo = await _usuarioRepository.SaveAsync(usuario);

            // Retorna o ViewModel para exibição
            return _mapper.Map<UsuarioExibicaoViewModel>(usuarioSalvo);
        }
    }
}
