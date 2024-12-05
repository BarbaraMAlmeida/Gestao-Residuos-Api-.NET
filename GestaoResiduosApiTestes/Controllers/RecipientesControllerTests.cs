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
    public class RecipientesControllerTests
    {
        private readonly Mock<IRecipienteService> _serviceMock;
        private readonly RecipientesController _controller;

        public RecipientesControllerTests()
        {
            _serviceMock = new Mock<IRecipienteService>();
            _controller = new RecipientesController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetAll_ValidRequest_ReturnsStatus200()
        {
            // Arrange
            var recipientes = new List<RecipienteExibicaoViewModel>
            {
                new RecipienteExibicaoViewModel { MaxCapacidade = 100, 
                    AtualNivel = 50, 
                    Status = StatusRecipiente.Vazio }
            };
            _serviceMock.Setup(s => s.GetPagedAsync(1, 2)).ReturnsAsync(recipientes);

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
            var model = new RecipienteCadastroViewModel
            {
                MaxCapacidade = 100,
                AtualNivel = 50,
                Status = StatusRecipiente.CapacidadeAtingida,
                UltimaAtualizacao = DateTime.Now,
                Latitude = 10,
                Longitude = 20
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

            _serviceMock.Setup(s => s.AddAsync(model)).Returns(Task.CompletedTask);

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
            var model = new RecipienteCadastroViewModel(); // Modelo vazio, inválido

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
            _controller.ModelState.AddModelError("MaxCapacidade", "MaxCapacidade é obrigatório.");
            _controller.ModelState.AddModelError("AtualNivel", "AtualNivel é obrigatório.");

            // Act
            var result = await _controller.Create(model);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
            Assert.Contains("MaxCapacidade", _controller.ModelState.Keys);
            Assert.Contains("AtualNivel", _controller.ModelState.Keys);
        }

    }
}
