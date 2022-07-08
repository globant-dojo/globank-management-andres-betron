using BankAPI.Models;
using BankAPICore.Controllers.Cuentas;
using BankAPICore.IData;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BankAPI.Tests.Controllers
{
    public class CuentaControllerTests
    {
        #region Global Arrangements
        private const int _idCuenta = 2;
        private const int _idCliente = 5;
        private readonly Mock<ICuentaDataService> _cuentaDataServiceMock;
        private readonly Mock<IClienteDataService> _clienteDataServiceMock;

        private CuentaController _cuentaController;
        private Cuenta _cuenta;
        private Cliente _cliente;

        #endregion

        public CuentaControllerTests()
        {
            _cuentaDataServiceMock = new Mock<ICuentaDataService>();
            _clienteDataServiceMock = new Mock<IClienteDataService>();

            _cuenta = new Cuenta()
            {
                IdCuenta = _idCuenta,
                IdCliente = _idCliente,
                Estado = 1,
                SaldoInicial = 100000,
                TipoCuenta = "Ahorro"
            };
            _cliente = new Cliente()
            {
                IdCliente = _idCliente,
                Contraseña = "1234",
                Estado = 1,
                IdPersona = 7
            };

            _cuentaController = new CuentaController(_cuentaDataServiceMock.Object,
                _clienteDataServiceMock.Object);
        }

        #region Get
        [Fact]
        public async Task Get_OnSuccess_ShouldReturnStatusCode200()
        {
            //Arrange
            _cuentaDataServiceMock.Setup(x => x.GetCuenta(_idCuenta))
                .ReturnsAsync(_cuenta);

            //Act
            var response = (OkObjectResult)await _cuentaController.Get(_idCuenta);

            //Assert
            response.StatusCode.Should().Be(200);

        }

        [Fact]
        public async Task Get_OnSuccess_InvokesCuentaDataServiceExactlyOnce()
        {
            //Arrange
            _cuentaDataServiceMock.Setup(x => x.GetCuenta(_idCuenta))
                .ReturnsAsync(_cuenta);

            //Act
            var response = (OkObjectResult)await _cuentaController.Get(_idCuenta);

            //Assert
            _cuentaDataServiceMock.Verify(
                service => service.GetCuenta(_idCuenta),
                Times.Once());
        }

        [Fact]
        public async Task Get_OnSuccess_ShouldReturnCuenta()
        {
            //Arrange
            _cuentaDataServiceMock.Setup(x => x.GetCuenta(_idCuenta))
                .ReturnsAsync(_cuenta);

            //Act
            var response = await _cuentaController.Get(_idCuenta);

            //Assert
            response.Should().BeOfType<OkObjectResult>();
            var objectResponse = (OkObjectResult)response;
            objectResponse.Value.Should().BeOfType<Cuenta>();
        }

        [Fact]
        public async Task Get_OnCuentaNotFound_ReturnsStatusCode404WithMessage()
        {
            //Arrange
            _cuentaDataServiceMock.Setup(x => x.GetCuenta(_idCuenta))
                .ReturnsAsync(new Cuenta());

            //Act
            var response = (NotFoundObjectResult)await _cuentaController.Get(_idCuenta);

            //Assert
            response.StatusCode.Should().Be(404);
            response.Value.Should().Be("La cuenta no ha sido encontrada.");
        }
        #endregion

        #region Post
        [Fact]
        public async Task Post_OnSuccess_ShouldReturnStatusCode200()
        {
            //Arrange
            _cuentaDataServiceMock.Setup(x => x.GetCuenta(_cuenta.IdCuenta))
                .ReturnsAsync(new Cuenta());
            _cuentaDataServiceMock.Setup(x => x.AddCuenta(_cuenta))
                .ReturnsAsync(true);

            //Act
            var response = (OkObjectResult)await _cuentaController.Post(_cuenta);

            //Assert
            response.StatusCode.Should().Be(200);

        }

        [Fact]
        public async Task Post_OnSuccess_ShouldReturnBool()
        {
            //Arrange
            _cuentaDataServiceMock.Setup(x => x.GetCuenta(_cuenta.IdCuenta))
                .ReturnsAsync(new Cuenta());
            _cuentaDataServiceMock.Setup(x => x.AddCuenta(_cuenta))
                .ReturnsAsync(true);

            //Act
            var response = await _cuentaController.Post(_cuenta);

            //Assert
            response.Should().BeOfType<OkObjectResult>();
            var objectResult = (OkObjectResult)response;
            objectResult.Value.Should().BeOfType<bool>();

        }

        [Fact]
        public async Task Post_OnSuccess_InvokesCuentaDataServiceTwice()
        {
            //Arrange
            _cuentaDataServiceMock.Setup(x => x.GetCuenta(_cuenta.IdCuenta))
                .ReturnsAsync(new Cuenta());
            _cuentaDataServiceMock.Setup(x => x.AddCuenta(_cuenta))
                .ReturnsAsync(true);

            //Act
            var response = await _cuentaController.Post(_cuenta);

            //Assert
            _cuentaDataServiceMock.Verify(
                service => service.GetCuenta(_cuenta.IdCuenta),
                Times.Once());
            _cuentaDataServiceMock.Verify(
                service => service.AddCuenta(_cuenta),
                Times.Once());
        }

        [Fact]
        public async Task Post_OnCuentaAlreadyExists_ShouldReturnStatusCode400WithMessage()
        {
            //Arrange
            _cuentaDataServiceMock.Setup(x => x.GetCuenta(_cuenta.IdCuenta))
                .ReturnsAsync(_cuenta);

            //Act
            var response = (BadRequestObjectResult)await _cuentaController.Post(_cuenta);

            //Assert
            response.StatusCode.Should().Be(400);
            response.Value.Should().Be("La cuenta ya existe, no puede ser insertada.");

        }

        [Fact]
        public async Task Post_OnCuentaNotInserted_ShouldReturnStatusCode400WithMessage()
        {
            //Arrange
            _cuentaDataServiceMock.Setup(x => x.GetCuenta(_cuenta.IdCuenta))
                .ReturnsAsync(new Cuenta());
            _cuentaDataServiceMock.Setup(x => x.AddCuenta(_cuenta))
                .ReturnsAsync(false);

            //Act
            var response = (BadRequestObjectResult)await _cuentaController.Post(_cuenta);

            //Assert
            response.StatusCode.Should().Be(400);
            response.Value.Should().Be("La cuenta no pudo ser insertada.");

        }

        #endregion

        #region Put
        [Fact]
        public async Task Put_OnSuccess_ShouldReturnStatusCode200()
        {
            //Arrange
            _cuentaDataServiceMock.Setup(x => x.GetCuenta(_idCuenta))
                .ReturnsAsync(_cuenta);
            _cuentaDataServiceMock.Setup(x => x.UpdateCuenta(_cuenta))
                .ReturnsAsync(true);

            _clienteDataServiceMock.Setup(x => x.GetCliente(_cuenta.IdCliente))
                .ReturnsAsync(_cliente);

            //Act
            var response = (OkObjectResult)await _cuentaController.Put(_cuenta);

            //Assert
            response.StatusCode.Should().Be(200);

        }

        [Fact]
        public async Task Put_OnSuccess_ShouldReturnBool()
        {
            //Arrange
            _cuentaDataServiceMock.Setup(x => x.GetCuenta(_cuenta.IdCuenta))
                .ReturnsAsync(_cuenta);
            _cuentaDataServiceMock.Setup(x => x.UpdateCuenta(_cuenta))
                .ReturnsAsync(true);

            _clienteDataServiceMock.Setup(x => x.GetCliente(_cuenta.IdCliente))
                .ReturnsAsync(_cliente);

            //Act
            var response = (OkObjectResult)await _cuentaController.Put(_cuenta);

            //Assert
            response.Value.Should().BeOfType<bool>();
        }

        [Fact]
        public async Task Put_OnSuccess_InvokesCuentaDataServiceTwiceAndClientDataServiceOnce()
        {
            //Arrange
            _cuentaDataServiceMock.Setup(x => x.GetCuenta(_cuenta.IdCuenta))
                .ReturnsAsync(_cuenta);
            _cuentaDataServiceMock.Setup(x => x.UpdateCuenta(_cuenta))
                .ReturnsAsync(true);

            _clienteDataServiceMock.Setup(x => x.GetCliente(_cuenta.IdCliente))
                .ReturnsAsync(_cliente);

            //Act
            var response = (OkObjectResult)await _cuentaController.Put(_cuenta);

            //Assert
            _cuentaDataServiceMock.Verify(
                service => service.GetCuenta(_cuenta.IdCuenta),
                Times.Once());
            _cuentaDataServiceMock.Verify(
                service => service.UpdateCuenta(_cuenta),
                Times.Once());

            _clienteDataServiceMock.Verify(
                service => service.GetCliente(_cuenta.IdCliente),
                Times.Once());
        }

        [Fact]
        public async Task Put_OnCuentaDoesntExists_ShouldReturnStatusCode404WithMessage()
        {
            //Arrange
            _cuentaDataServiceMock.Setup(x => x.GetCuenta(_cuenta.IdCuenta))
                .ReturnsAsync(new Cuenta());

            //Act
            var response = (BadRequestObjectResult)await _cuentaController.Put(_cuenta);

            //Assert
            response.StatusCode.Should().Be(400);
            response.Value.Should().Be("La cuenta no existe.");
        }

        [Fact]
        public async Task Put_OnClientDoesntExists_ShouldReturnStatusCode404WithMessage()
        {
            //Arrange
            _cuentaDataServiceMock.Setup(x => x.GetCuenta(_cuenta.IdCuenta))
                .ReturnsAsync(_cuenta);
            _cuentaDataServiceMock.Setup(x => x.UpdateCuenta(_cuenta))
                .ReturnsAsync(true);

            _clienteDataServiceMock.Setup(x => x.GetCliente(_cuenta.IdCliente))
                .ReturnsAsync(new Cliente());

            //Act
            var response = (BadRequestObjectResult)await _cuentaController.Put(_cuenta);

            //Assert
            response.StatusCode.Should().Be(400);
            response.Value.Should().Be("El cliente no existe.");
        }


        #endregion

        #region Delete
        [Fact]
        public async Task Delete_OnSuccess_ShouldReturnStatusCode200()
        {
            //Arrange
            _cuentaDataServiceMock.Setup(x => x.GetCuenta(_idCuenta))
                .ReturnsAsync(_cuenta);
            _cuentaDataServiceMock.Setup(x => x.DeleteCuenta(_cuenta))
                .ReturnsAsync(true);

            //Act
            var response = (OkObjectResult)await _cuentaController.Delete(_idCuenta);

            //Assert
            response.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Delete_OnSuccess_ShouldReturnBool()
        {
            //Arrange
            _cuentaDataServiceMock.Setup(x => x.GetCuenta(_idCuenta))
                .ReturnsAsync(_cuenta);
            _cuentaDataServiceMock.Setup(x => x.DeleteCuenta(_cuenta))
                .ReturnsAsync(true);

            //Act
            var response = (OkObjectResult)await _cuentaController.Delete(_idCuenta);

            //Assert
            response.Value.Should().BeOfType<bool>();
        }

        [Fact]
        public async Task Delete_OnSuccess_InvokesCuentaDataServiceExactlyTwice()
        {
            //Arrange
            _cuentaDataServiceMock.Setup(x => x.GetCuenta(_idCuenta))
                .ReturnsAsync(_cuenta);
            _cuentaDataServiceMock.Setup(x=>x.DeleteCuenta(_cuenta))
                .ReturnsAsync(true);

            //Act
            var response = (OkObjectResult)await _cuentaController.Delete(_idCuenta);

            //Assert
            _cuentaDataServiceMock.Verify(
                service => service.GetCuenta(_idCuenta),
                Times.Once());
            _cuentaDataServiceMock.Verify(
                service => service.DeleteCuenta(_cuenta),
                Times.Once());
        }

        [Fact]
        public async Task Delete_OnCuentaNotFound_ShouldReturnStatusCode404WithMessage()
        {
            //Arrange
            _cuentaDataServiceMock.Setup(x => x.GetCuenta(_idCuenta))
                .ReturnsAsync(new Cuenta());

            //Act
            var response = (NotFoundObjectResult)await _cuentaController.Delete(_idCuenta);

            //Assert
            response.StatusCode.Should().Be(404);
            response.Value.Should().Be("La cuenta no existe.");
        }

        #endregion
    }
}
