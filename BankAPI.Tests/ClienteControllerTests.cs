using BankAPI.Models;
using BankAPICore.Data;
using Moq;
using Xunit;

namespace BankAPI.Tests
{
    public class ClienteControllerTests
    {
        private const int _idCliente = 5;
        private const int _idPersona = 7;
        private static Persona _persona = new Persona
        {
            IdPersona = _idPersona,
            Nombre = "Andres",
            Direccion = "Avenida Cero",
            Edad = 26,
            Genero = "Masculino",
            Identificacion = "11111111",
            Telefono = "33564323"
        };

        private static Cliente _clienteConPersona = new Cliente
        {
            IdCliente = _idCliente,
            Contraseña = "1234",
            Estado = 1,
            IdPersona = _idPersona
        };
        private Mock<IClienteDataService> _clienteDataService;

        public ClienteControllerTests(Mock<IClienteDataService> clienteDataService)
        {
            _clienteDataService = new Mock<IClienteDataService>();
        }

        [Fact]

        public void ShouldSaveClientIfPersonaExist()
        {
            //Arrange

            //Act

            //Assert
        }
    }
}