using BankAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankAPICore.Controllers.Movimientos
{
    [ApiController]
    [Route("[controller]")]
    public class MovimientoController : ControllerBase
    {
        private readonly ILogger<MovimientoController> _logger;

        public MovimientoController(ILogger<MovimientoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Movimiento Get()
        {
            return new Movimiento();
        }
    }
}
