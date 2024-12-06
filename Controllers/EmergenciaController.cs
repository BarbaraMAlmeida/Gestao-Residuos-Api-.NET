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
        public async Task<IActionResult> GetAll()
        {
            var emergencia = await _service.GetAllAsync();

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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var emergencia = await _service.AddAsync(model);
                return CreatedAtAction(nameof(GetAll), null, emergencia); // Retorna 201 Created com o recurso criado
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
