using GestaoResiduosApi.Services;
using GestaoResiduosApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestaoResiduosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
        public class RotaController : ControllerBase
        {
            //private readonly IRotaService _service;

            //public RotaController(IRotaService service)
            //{
            //    _service = service;
            //}

            //// Endpoint GET - Paginação
            //[Authorize(Policy = "UserOrAdminPolicy")]
            //[HttpGet]
            //public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
            //{
            //    if (pageNumber <= 0 || pageSize <= 0) {
            //        return BadRequest("Número da página e tamanho da página devem ser maiores que zero.");
            //    }

            //    var rotas = await _service.GetPagedAsync(pageNumber, pageSize);

            //    if (!rotas.Any()){
            //        return NoContent();
            //    }

            //    return Ok(rotas);
            //}

            //// Endpoint POST
            //[Authorize(Policy = "AdminPolicy")]
            //[HttpPost]
            //public async Task<IActionResult> Create([FromBody] RotaCadastroViewModel model)
            //{
            //    if (!ModelState.IsValid)
            //    {
            //        return BadRequest(ModelState);
            //    }

            //   await _service.AddAsync(model);
            //   return CreatedAtAction(nameof(GetAll), null);
            //}
        }
 }
