using Microsoft.AspNetCore.Mvc;

namespace BankAPICore.Controllers.Cuentas
{
    public class CuentaController : Controller
    {
        private readonly ILogger<CuentaController> _logger;

        public CuentaController(ILogger<CuentaController> logger)
        {
            _logger = logger;
        }
    }
}
