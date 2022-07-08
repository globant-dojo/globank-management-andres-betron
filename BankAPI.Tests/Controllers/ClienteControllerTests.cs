using BankAPI.Models;
using BankAPICore.Controllers.Clients;
using BankAPICore.Data;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BankAPI.Tests
{
    public class ClienteControllerTests
    {
        //Arrange
        private const int _idCliente = 5;
        private const int _idPersona = 7;
        private readonly Mock<IClienteDataService> _clienteDataServiceMock;
        private readonly Mock<IPersonaDataService> _personaDataServiceMock;
        private Cliente _cliente;
        private ClienteController _clienteController;

        public ClienteControllerTests()
        {
            _clienteDataServiceMock = new Mock<IClienteDataService>();
            _personaDataServiceMock = new Mock<IPersonaDataService>();


            _cliente = new Cliente()
            {
                IdCliente = _idCliente,
                Contraseña = "1234",
                Estado = 1,
                IdPersona = _idPersona
            };
            _clienteController = new ClienteController(_clienteDataServiceMock.Object,
                _personaDataServiceMock.Object);
        }

        #region Get
        [Fact]
        public async Task Get_OnSuccess_ReturnsStatusCode200()
        {
            //Arrange
            _clienteDataServiceMock.Setup(x => x.GetCliente(_idCliente))
                .ReturnsAsync(_cliente); ;

            //Act
            var result = (OkObjectResult)await _clienteController.Get(_idCliente);

            //Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Get_OnSuccess_InvokesClienteDataServiceExactlyOnce()
        {
            //Arrange
            _clienteDataServiceMock.Setup(x => x.GetCliente(_idCliente))
                .ReturnsAsync(_cliente); ;

            //Act
            var result = (OkObjectResult)await _clienteController.Get(_idCliente);

            //Assert
            _clienteDataServiceMock.Verify(
                service => service.GetCliente(_idCliente),
                Times.Once());

        }

        [Fact]
        public async Task Get_OnSucces_ReturnsCliente()
        {
            //Arrange
            _clienteDataServiceMock.Setup(x => x.GetCliente(_idCliente))
                .ReturnsAsync(_cliente); ;

            //Act
            var result = await _clienteController.Get(_idCliente);

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            var objectResult = (OkObjectResult)result;
            objectResult.Value.Should().BeOfType<Cliente>();
        }

        [Fact]
        public async Task Get_OnNoClienteFound_Returns404()
        {
            //Arrange
            _clienteDataServiceMock.Setup(x => x.GetCliente(_idCliente))
                .ReturnsAsync(new Cliente());

            //Act
            var result = await _clienteController.Get(_idCliente);

            //Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        #endregion

        #region Post
        [Fact]
        public async Task Post_OnSuccess_ReturnsStatusCode200()
        {
            //Arrange
            _clienteDataServiceMock.Setup(x => x.InsertCliente(_cliente))
                .ReturnsAsync(true);

            _personaDataServiceMock.Setup(x => x.InsertPersona(_cliente.Persona))
                .ReturnsAsync(true);

            //Act
            var result = (OkObjectResult)await _clienteController.Post(_cliente);

            //Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Post_OnSuccess_InvokesPersonaDataServiceTwiceWhenPersonaInserted()
        {
            //Arrange
            _clienteDataServiceMock.Setup(x => x.InsertCliente(_cliente))
                .ReturnsAsync(true);

            _personaDataServiceMock.Setup(x => x.GetPersona(_cliente.IdPersona))
                .ReturnsAsync(new Persona());
            _personaDataServiceMock.Setup(x => x.InsertPersona(_cliente.Persona))
                .ReturnsAsync(true);

            //Act
            var result = (OkObjectResult)await _clienteController.Post(_cliente);

            //Assert
            _personaDataServiceMock.Verify(
                service => service.GetPersona(_cliente.IdPersona),
                Times.Once());
            _personaDataServiceMock.Verify(
                service => service.InsertPersona(_cliente.Persona),
                Times.Once());
        }

        [Fact]
        public async Task Post_OnSuccess_InvokesPersonaDataServiceTwiceWhenPersonaUpdated()
        {
            //Arrange
            _clienteDataServiceMock.Setup(x => x.InsertCliente(_cliente))
                .ReturnsAsync(true);

            _personaDataServiceMock.Setup(x => x.GetPersona(_cliente.IdPersona))
                .ReturnsAsync(new Persona()
                {
                    IdPersona = _cliente.IdPersona,
                    Direccion = "Avenida Cero",
                    Edad = 20,
                    Genero = "Masculino",
                    Identificacion = "100000",
                    Nombre = "Nombre",
                    Telefono = "00400040"
                });
            _personaDataServiceMock.Setup(x => x.UpdatePersona(_cliente.Persona))
                .Returns(true);

            //Act
            var result = (OkObjectResult)await _clienteController.Post(_cliente);

            //Assert
            _personaDataServiceMock.Verify(
                service => service.GetPersona(_cliente.IdPersona),
                Times.Once());
            _personaDataServiceMock.Verify(
                service => service.UpdatePersona(_cliente.Persona),
                Times.Once());
        }

        [Fact]
        public async Task Post_OnSuccess_InvokesClienteDataServiceExactlyOnce()
        {
            //Arrange
            _clienteDataServiceMock.Setup(x => x.InsertCliente(_cliente))
                .ReturnsAsync(true);

            _personaDataServiceMock.Setup(x => x.InsertPersona(_cliente.Persona))
                .ReturnsAsync(true);

            //Act
            var result = (OkObjectResult)await _clienteController.Post(_cliente);

            //Assert
            _clienteDataServiceMock.Verify(
                service => service.InsertCliente(_cliente),
                Times.Once());

        }

        [Fact]
        public async Task Post_OnSucces_ReturnsBool()
        {
            //Arrange
            _clienteDataServiceMock.Setup(x => x.InsertCliente(_cliente))
                .ReturnsAsync(true);

            _personaDataServiceMock.Setup(x => x.InsertPersona(_cliente.Persona))
                .ReturnsAsync(true);

            //Act
            var result = await _clienteController.Post(_cliente);

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            var objectResult = (OkObjectResult)result;
            objectResult.Value.Should().BeOfType<bool>();
        }

        [Fact]
        public async Task Post_OnPersonaNotInserted_ReturnsStatusCode400WithMessage()
        {
            //Arrange
            _personaDataServiceMock.Setup(x => x.InsertPersona(_cliente.Persona))
                .ReturnsAsync(false);

            //Act
            var result = await _clienteController.Post(_cliente);

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var objectResult = (BadRequestObjectResult)result;
            objectResult.Value.Should().Be("La persona no pudo ser insertada.");
        }

        [Fact]
        public async Task Post_OnClienteNotInserted_ReturnsStatusCode400WithMessage()
        {
            //Arrange
            _clienteDataServiceMock.Setup(x => x.InsertCliente(_cliente))
                .ReturnsAsync(false);

            _personaDataServiceMock.Setup(x => x.InsertPersona(_cliente.Persona))
                .ReturnsAsync(true);

            //Act
            var result = await _clienteController.Post(_cliente);

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var objectResult = (BadRequestObjectResult)result;
            objectResult.Value.Should().Be("El cliente no puedo ser insertado.");
        }

        #endregion

    }
}