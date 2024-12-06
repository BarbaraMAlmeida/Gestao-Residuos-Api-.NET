using System.Collections.Generic;
using System.Threading.Tasks;
using GestaoResiduosApi.Controllers;
using GestaoResiduosApi.Enums;
using GestaoResiduosApi.Services;
using GestaoResiduosApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Moq;
using Xunit;

namespace GestaoResiduosApiTestes.Controllers
{
    public class AgendamentosControllerTests
    {
        private readonly Mock<IAgendamentoService> _serviceMock;
        private readonly AgendamentosController _controller;

        public AgendamentosControllerTests()
        {
            _serviceMock = new Mock<IAgendamentoService>();
            _controller = new AgendamentosController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetAll_ValidRequest_ReturnsStatus200()
        {
            // Arrange
            var agendamentos = new List<AgendamentoExibicaoViewModel>
            {
                new AgendamentoExibicaoViewModel
                {
                    IdAgendamento = 1,
                    DtAgendamento = DateTime.Now,
                    StatusAgendamento = StatusAgendamento.Realizado,
                    RotaId = 1001
                }
            };
            _serviceMock.Setup(s => s.GetPagedAsync(1, 2)).ReturnsAsync(agendamentos);

            // Act
            var result = await _controller.GetAll(1, 2);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task GetAll_InvalidRequest_ReturnsStatus400()
        {
            // Act
            var result = await _controller.GetAll(0, 0);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task Create_ValidModel_ReturnsStatus201()
        {
            // Arrange
            var model = new AgendamentoCadastroViewModel
            {
                DtAgendamento = DateTime.Now,
                StatusAgendamento = StatusAgendamento.Realizado,
                RotaId = 1001
            };

            var mockValidator = new Mock<IObjectModelValidator>();
            mockValidator.Setup(v => v.Validate(
                It.IsAny<ActionContext>(),
                It.IsAny<ValidationStateDictionary>(),
                It.IsAny<string>(),
                It.IsAny<object>())
            );

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            _controller.ObjectValidator = mockValidator.Object;

            if (!_controller.TryValidateModel(model))
            {
                _controller.ModelState.AddModelError("Model", "Modelo inválido.");
            }

            _serviceMock.Setup(s => s.CreateAsync(model)).ReturnsAsync(new AgendamentoExibicaoViewModel
            {
                IdAgendamento = 1,
                DtAgendamento = model.DtAgendamento,
                StatusAgendamento = model.StatusAgendamento,
                RotaId = model.RotaId
            });

            // Act
            var result = await _controller.Create(model);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(201, createdAtActionResult.StatusCode);
        }

        [Fact]
        public async Task Create_InvalidModel_ReturnsStatus400()
        {
            // Arrange
            var model = new AgendamentoCadastroViewModel(); // Modelo vazio, inválido

            var mockValidator = new Mock<IObjectModelValidator>();
            mockValidator.Setup(v => v.Validate(
                It.IsAny<ActionContext>(),
                It.IsAny<ValidationStateDictionary>(),
                It.IsAny<string>(),
                It.IsAny<object>())
            );

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            _controller.ObjectValidator = mockValidator.Object;
            _controller.ModelState.AddModelError("DtAgendamento", "DtAgendamento é obrigatório.");
            _controller.ModelState.AddModelError("RotaId", "RotaId é obrigatório.");

            // Act
            var result = await _controller.Create(model);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
            Assert.Contains("DtAgendamento", _controller.ModelState.Keys);
            Assert.Contains("RotaId", _controller.ModelState.Keys);
        }
    }
}
