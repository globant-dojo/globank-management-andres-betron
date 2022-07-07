using Microsoft.AspNetCore.Mvc;

namespace BankAPICore.Controllers.Movimientos
{
    public class MovimientoController : Controller
    {
        private readonly ILogger<MovimientoController> _logger;

        public MovimientoController(ILogger<MovimientoController> logger)
        {
            _logger = logger;
        }
    }
}
