using GestaoResiduosApi.Controllers;
using GestaoResiduosApi.Enums;
using GestaoResiduosApi.Services;
using GestaoResiduosApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoResiduosApiTestes.Controllers
{
    public class CaminhaoControllerTests
    {
        private readonly Mock<ICaminhaoService> _serviceMock;
        private readonly CaminhaoController _controller;

        public CaminhaoControllerTests()
        {
            _serviceMock = new Mock<ICaminhaoService>();
            _controller = new CaminhaoController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetAll_ValidRequest_ReturnsStatus200()
        {
            // Arrange
            var caminhoes = new List<CaminhaoExibicaoViewModel>
            {
                new CaminhaoExibicaoViewModel { Placa = "AXT5432",
                    Capacidade = 50}
            };
            _serviceMock.Setup(s => s.GetPagedAsync(1, 2)).ReturnsAsync(caminhoes);

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
            var model = new CaminhaoCadastroViewModel
            {
                Capacidade = 100,
                Placa = "AXT5432"
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
            var model = new CaminhaoCadastroViewModel(); // Modelo vazio, inválido

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
            _controller.ModelState.AddModelError("Capacidade", "Capacidade é obrigatória.");
            _controller.ModelState.AddModelError("Placa", "Placa é obrigatória.");

            // Act
            var result = await _controller.Create(model);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
            Assert.Contains("Capacidade", _controller.ModelState.Keys);
            Assert.Contains("Placa", _controller.ModelState.Keys);
        }
    }
}