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
    public class RotaControllerTests
    {
        private readonly Mock<IRotaService> _serviceMock;
        private readonly RotaController _controller;

        public RotaControllerTests()
        {
            _serviceMock = new Mock<IRotaService>();
            _controller = new RotaController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetAll_ValidRequest_ReturnsStatus200()
        {
            // Arrange
            var rota = new List<RotaExibicaoViewModel>
            {
                new RotaExibicaoViewModel
                {
                    IdRota = 1,
                    DtRota = DateTime.Now,
                    RecipienteId = 1,
                    CaminhaoId = 1
                }
            };
            _serviceMock.Setup(s => s.GetPagedAsync(1, 2)).ReturnsAsync(rota);

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
            var model = new RotaCadastroViewModel
            {
                DtRota = DateTime.Now,
                RecipienteId = 1,
                CaminhaoId = 1
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

            _serviceMock.Setup(s => s.CreateAsync(model)).ReturnsAsync(new RotaExibicaoViewModel
            {
                IdRota = 1,
                DtRota = DateTime.Now,
                RecipienteId = 1,
                CaminhaoId = 1
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
            var model = new RotaCadastroViewModel(); // Modelo vazio, inválido

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
            _controller.ModelState.AddModelError("DtRota", "DtRota é obrigatório.");
            _controller.ModelState.AddModelError("CaminhaoId", "CaminhaoId é obrigatório.");
            _controller.ModelState.AddModelError("RecipienteId", "RecipienteId é obrigatório.");

            // Act
            var result = await _controller.Create(model);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
            Assert.Contains("DtRota", _controller.ModelState.Keys);
            Assert.Contains("CaminhaoId", _controller.ModelState.Keys);
            Assert.Contains("RecipienteId", _controller.ModelState.Keys);
        }
    }
}

