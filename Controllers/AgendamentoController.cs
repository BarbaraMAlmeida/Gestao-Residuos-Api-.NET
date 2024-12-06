using GestaoResiduosApi.Services;
using GestaoResiduosApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestaoResiduosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgendamentosController : ControllerBase
    {
        private readonly IAgendamentoService _service;

        public AgendamentosController(IAgendamentoService service)
        {
            _service = service;
        }

        // GET: Paginação de agendamentos
        [Authorize(Policy = "UserOrAdminPolicy")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Número da página e tamanho da página devem ser maiores que zero.");
            }

            var agendamentos = await _service.GetPagedAsync(pageNumber, pageSize);

            if (!agendamentos.Any())
            {
                return NoContent();
            }

            return Ok(agendamentos);
        }

        // POST: Cadastro de agendamento
        [Authorize(Policy = "AdminPolicy")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AgendamentoCadastroViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Errors = ModelState
                        .Where(ms => ms.Value.Errors.Any())
                        .ToDictionary(
                            ms => ms.Key,
                            ms => ms.Value.Errors.Select(e => e.ErrorMessage)
                        )
                });
            }

            try
            {
                var result = await _service.CreateAsync(model);
                return CreatedAtAction(nameof(GetAll), new { id = result.IdAgendamento }, result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    Errors = ex.Message.Split(" | ").Select(e => new { Message = e })
                });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Error = "Ocorreu um erro inesperado ao processar sua solicitação."
                });
            }
        }

        // PUT: Atualização de agendamento
        [Authorize(Policy = "AdminPolicy")]
        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, [FromBody] AgendamentoCadastroViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Errors = ModelState
                        .Where(ms => ms.Value.Errors.Any())
                        .ToDictionary(
                            ms => ms.Key,
                            ms => ms.Value.Errors.Select(e => e.ErrorMessage)
                        )
                });
            }

            try
            {
                var result = await _service.UpdateAsync(model, id);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Error = ex.Message });
            }
        }

        // DELETE: Exclusão de agendamento
        [Authorize(Policy = "AdminPolicy")]
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success)
            {
                return NotFound(new { Error = "Agendamento não encontrado." });
            }

            return NoContent();
        }
    }
}
