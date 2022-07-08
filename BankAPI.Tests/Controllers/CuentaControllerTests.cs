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

        private CuentaController _cuentaController;
        private Cuenta _cuenta;

        #endregion

        public CuentaControllerTests()
        {
            _cuentaDataServiceMock = new Mock<ICuentaDataService>();

            _cuenta = new Cuenta()
            {
                IdCuenta = _idCuenta,
                IdCliente = _idCliente,
                Estado = 1,
                SaldoInicial = 100000,
                TipoCuenta = "Ahorro"
            };

            _cuentaController = new CuentaController(_cuentaDataServiceMock.Object);
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
    }
}
