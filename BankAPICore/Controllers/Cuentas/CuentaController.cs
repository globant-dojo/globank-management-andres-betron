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
        private readonly IClienteDataService _clienteDataService;

        public CuentaController(ICuentaDataService cuentaDataService, 
            IClienteDataService clienteDataService)
        {
            _cuentaDataService = cuentaDataService;
            _clienteDataService = clienteDataService;
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

        [HttpPut]
        public async Task<IActionResult> Put(Cuenta cuenta)
        {
            try
            {
                var cuentaExists = await _cuentaDataService.GetCuenta(cuenta.IdCuenta);
                if (cuentaExists == null ||
                    string.IsNullOrEmpty(cuentaExists.TipoCuenta))
                    return BadRequest("La cuenta no existe.");

                var clienteExists = await _clienteDataService.GetCliente(cuenta.IdCliente);
                if (clienteExists == null ||
                   string.IsNullOrEmpty(clienteExists.Contraseña))
                    return BadRequest("El cliente no existe.");

                var cuentaUpdated = await _cuentaDataService.UpdateCuenta(cuenta);
                return Ok(cuentaUpdated);
            }
            catch (Exception)
            {
                return StatusCode(501, "Unhandled Exception");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int idCuenta)
        {
            try
            {
                var cuentaExists = await _cuentaDataService.GetCuenta(idCuenta);
                if (cuentaExists == null ||
                   string.IsNullOrEmpty(cuentaExists.TipoCuenta))
                    return NotFound("La cuenta no existe.");

                var cuentaDeleted = await _cuentaDataService.DeleteCuenta(cuentaExists);
                return Ok(cuentaDeleted);
            }
            catch (Exception)
            {
                return StatusCode(501, "Unhandled Exception");
            }
        }
    }
}
