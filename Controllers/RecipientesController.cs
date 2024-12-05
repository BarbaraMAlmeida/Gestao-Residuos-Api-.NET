using GestaoResiduosApi.Services;
using GestaoResiduosApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestaoResiduosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipientesController : ControllerBase
    {
        private readonly IRecipienteService _service;

        public RecipientesController(IRecipienteService service)
        {
            _service = service;
        }

        [Authorize(Policy = "UserOrAdminPolicy")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 2)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Número da página e tamanho da página devem ser maiores que zero.");
            }

            var recipientes = await _service.GetPagedAsync(pageNumber, pageSize);

            if (!recipientes.Any())
            {
                return NoContent();
            }

            return Ok(recipientes);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RecipienteCadastroViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Log para garantir que o ModelState foi detectado como inválido
                Console.WriteLine("ModelState inválido: " + string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)));

                // Retorna os erros de validação
                return BadRequest(ModelState);
            }

            await _service.AddAsync(model);
            return CreatedAtAction(nameof(GetAll), null); // Retorna 201 Created
        }


    }
}
