using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GestaoResiduosApi.Controllers;
using GestaoResiduosApi.Enums;
using GestaoResiduosApi.Models;
using GestaoResiduosApi.Services;
using GestaoResiduosApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Moq;
using Xunit;

namespace GestaoResiduosApi.Tests
{
    public class EmergenciaControllerTests
    {
        private readonly Mock<IEmergenciaService> _serviceMock;
        private readonly EmergenciaController _controller;

        public EmergenciaControllerTests()
        {
            _serviceMock = new Mock<IEmergenciaService>();
            _controller = new EmergenciaController(_serviceMock.Object);
        }

        [Fact]
        public async Task Create_ValidModel_ReturnsStatus201()
        {
            // Arrange
            var viewModel = new EmergenciaCadastroViewModel
            {
                DtEmergencia = DateTime.Parse("2022-03-01"),
                Status = StatusEmergencia.Coletada,
                Descricao = "Emergência teste",
                RecipienteId = 1,
                CaminhaoId = 2
            };

            var mockValidator = new Mock<IObjectModelValidator>();
            mockValidator.Setup(v => v.Validate(
                It.IsAny<ActionContext>(),
                It.IsAny<ValidationStateDictionary>(),
                It.IsAny<string>(),
                It.IsAny<object>()));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            _controller.ObjectValidator = mockValidator.Object;

            _serviceMock.Setup(s => s.AddAsync(viewModel)).ReturnsAsync(new EmergenciaExibicaoViewModel
            {
                DtEmergencia = viewModel.DtEmergencia,
                Status = viewModel.Status,
                Descricao = viewModel.Descricao,
                RecipienteId = viewModel.RecipienteId,
                CaminhaoId = viewModel.CaminhaoId
            });

            // Act
            var result = await _controller.Create(viewModel);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(201, createdAtActionResult.StatusCode);
        }

        [Fact]
        public async Task Create_InvalidModel_ReturnsStatus400()
        {
            // Arrange
            var viewModel = new EmergenciaCadastroViewModel(); // Modelo vazio, inválido

            var mockValidator = new Mock<IObjectModelValidator>();
            mockValidator.Setup(v => v.Validate(
                It.IsAny<ActionContext>(),
                It.IsAny<ValidationStateDictionary>(),
                It.IsAny<string>(),
                It.IsAny<object>()));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            _controller.ObjectValidator = mockValidator.Object;

            _controller.ModelState.AddModelError("Descricao", "A descrição é obrigatória.");

            // Act
            var result = await _controller.Create(viewModel);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
            Assert.Contains("Descricao", _controller.ModelState.Keys);
        }
    }
}