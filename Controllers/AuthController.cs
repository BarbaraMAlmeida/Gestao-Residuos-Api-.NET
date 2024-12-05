using GestaoResiduosApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GestaoResiduosApi.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace GestaoResiduosApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly TokenService _tokenService;
        private readonly IUsuarioService _usuarioService;

        public AuthController(TokenService tokenService, IUsuarioService usuarioService)
        {
            _tokenService = tokenService;
            _usuarioService = usuarioService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginDto)
        {
            var usuario = await _usuarioService.AuthenticateAsync(loginDto.Email, loginDto.Senha);
            if (usuario == null)
            {
                return Unauthorized("Usuário ou senha inválidos");
            }

            var token = _tokenService.GenerateToken(usuario.Email, usuario.Role.ToString());
            return Ok(new { Token = token });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UsuarioCadastroViewModel usuarioCadastroDto)
        {
            var usuario = await _usuarioService.SalvarUsuario(usuarioCadastroDto);
            return Created("", usuario);
        }
    }
}
