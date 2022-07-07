using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BankAPICore.Controllers.Movimientos
{
    [ApiController]
    [Route("[controller]")]
    public class MovimientosController : Controller
    {
        private readonly ILogger<MovimientosController> _logger;

        public MovimientosController(ILogger<MovimientosController> logger)
        {
            _logger = logger;
        }
    }
}
