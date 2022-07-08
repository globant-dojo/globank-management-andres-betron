using BankAPICore.Controllers.Clients;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace BankAPI.Tests
{
    public class ClienteControllerTests
    {

        [Fact]
        public async Task GetOnSuccessReturnsOk()
        {
            //Arrange
            var clienteController = new ClienteController();

            //Act
            var result = (OkObjectResult)await clienteController.Get();

            //Assert
            Assert.Equal(200, result.StatusCode);
        }

    }
}