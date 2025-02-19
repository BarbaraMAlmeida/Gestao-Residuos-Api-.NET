﻿using GestaoResiduosApi.Services;
using GestaoResiduosApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace GestaoResiduosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RotaController: ControllerBase
    {
        private readonly IRotaService _service;

        public RotaController(IRotaService service)
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

            var rotas = await _service.GetPagedAsync(pageNumber, pageSize);

            if (!rotas.Any())
            {
                return NoContent();
            }

            return Ok(rotas);
        }

        // POST: Cadastro de agendamento
        [Authorize(Policy = "AdminPolicy")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RotaCadastroViewModel model)
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
                return CreatedAtAction(nameof(GetAll), new { id = result.IdRota }, result);
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
    }
}
