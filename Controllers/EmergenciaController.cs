using GestaoResiduosApi.Services;
using GestaoResiduosApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestaoResiduosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmergenciaController : ControllerBase
    {
        private readonly IEmergenciaService _service;

        public EmergenciaController(IEmergenciaService service)
        {
            _service = service;
        }

        [Authorize(Policy = "UserOrAdminPolicy")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Número da página e tamanho da página devem ser maiores que zero.");
            }

            var emergencia = await _service.GetPagedAsync(pageNumber, pageSize);

            if (!emergencia.Any())
            {
                return NoContent();
            }

            return Ok(emergencia);
        }

        [Authorize(Policy = "AdminPolicy")]

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmergenciaCadastroViewModel model)
        {
            // Validação manual para garantir que todos os campos obrigatórios sejam verificados
            var erros = new List<string>();

            if (model.DtEmergencia == default)
            {
                erros.Add("A Data da emergência é obrigatória.");
            }

            if (model.Status == default)
            {
                erros.Add("O Status da emergência é obrigatório.");
            }

            if (!ModelState.IsValid)
            {
                erros.AddRange(ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)));
            }

            if (erros.Any())
            {
                return BadRequest(new { Message = "Erro de validação.", Erros = erros });
            }

            try
            {
                var emergencia = await _service.AddAsync(model);
                return CreatedAtAction(nameof(GetAll), null, emergencia);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                // Retorno genérico para erros inesperados
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Ocorreu um erro ao processar a solicitação.", Detalhes = ex.Message });
            }
        }
    }
}
