using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BankAPICore.Controllers.Cuentas
{
    [ApiController]
    [Route("[controller]")]
    public class CuentasController : Controller
    {
        private readonly ILogger<CuentasController> _logger;

        public CuentasController(ILogger<CuentasController> logger)
        {
            _logger = logger;
        }
    }
}
