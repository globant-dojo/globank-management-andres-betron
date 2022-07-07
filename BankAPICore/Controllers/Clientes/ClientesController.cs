using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BankAPICore.Controllers.Clientes
{
    [ApiController]
    [Route("[controller]")]
    public class ClientesController : Controller
    {
        private readonly ILogger<ClientesController> _logger;

        public ClientesController(ILogger<ClientesController> logger)
        {
            _logger = logger;
        }
    }
}
