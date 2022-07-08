using Microsoft.AspNetCore.Mvc;

namespace BankAPICore.Controllers.Cuentas
{
    [ApiController]
    [Route("[controller]")]
    public class ReportesController : ControllerBase
    {
        private readonly ILogger<ReportesController> _logger;

        public ReportesController(ILogger<ReportesController> logger)
        {
            _logger = logger;
        }
    }
}
