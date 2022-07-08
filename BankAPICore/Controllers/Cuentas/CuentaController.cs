using BankAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankAPICore.Controllers.Cuentas
{
    [ApiController]
    [Route("[controller]")]
    public class CuentaController : ControllerBase
    {
        private readonly ILogger<CuentaController> _logger;

        public CuentaController(ILogger<CuentaController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        public Cuenta Get()
        {
            return new Cuenta();
        }
    }
}
