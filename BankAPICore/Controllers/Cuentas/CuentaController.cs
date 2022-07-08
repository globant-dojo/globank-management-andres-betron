using BankAPI.Models;
using BankAPICore.IData;
using Microsoft.AspNetCore.Mvc;

namespace BankAPICore.Controllers.Cuentas
{
    [ApiController]
    [Route("[controller]")]
    public class CuentaController : ControllerBase
    {
        private readonly ICuentaDataService _cuentaDataService;

        public CuentaController(ICuentaDataService cuentaDataService)
        {
            _cuentaDataService = cuentaDataService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int idCuenta)
        {
            try
            {
                var cuentaEncontrada = await _cuentaDataService.GetCuenta(idCuenta);
                if(cuentaEncontrada == null ||
                   string.IsNullOrEmpty(cuentaEncontrada.TipoCuenta))
                    return NotFound("La cuenta no ha sido encontrada.");

                return Ok(cuentaEncontrada);
            }
            catch (Exception)
            {
                return StatusCode(501, "Unhandled Exception");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Cuenta cuenta)
        {
            try
            {
                var cuentaExists = await _cuentaDataService.GetCuenta(cuenta.IdCuenta);
                if (cuentaExists != null &&
                    !string.IsNullOrEmpty(cuentaExists.TipoCuenta))
                    return BadRequest("La cuenta ya existe, no puede ser insertada.");
                var cuentaInserted = await _cuentaDataService.AddCuenta(cuenta);
                if(!cuentaInserted)
                    return BadRequest("La cuenta no pudo ser insertada.");
                return Ok(cuentaInserted);
            }
            catch (Exception)
            {
                return StatusCode(501, "Unhandled Exception");
            }
        }
    }
}
