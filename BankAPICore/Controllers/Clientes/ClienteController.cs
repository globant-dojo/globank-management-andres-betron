using Microsoft.AspNetCore.Mvc;

namespace BankAPICore.Controllers.Clients
{
    public class ClienteController : Controller
    {
        private readonly ILogger<ClienteController> _logger;

        public ClienteController(ILogger<ClienteController> logger)
        {
            _logger = logger;
        }
    }
}
