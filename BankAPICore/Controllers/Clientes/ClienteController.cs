using BankAPI.Models;
using BankAPICore.Data;
using Microsoft.AspNetCore.Mvc;

namespace BankAPICore.Controllers.Clients
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly ILogger<ClienteController> _logger;
        private readonly IClienteDataService _clienteDataService;

        public ClienteController()//ILogger<ClienteController> logger,
            //IClienteDataService clienteDataService)
        {
            //_logger = logger;
            //_clienteDataService = clienteDataService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return null;
                //var cliente = _clienteDataService.GetCliente(0);
                //if (cliente == null)
                //    return NotFound();

                //return Ok(cliente);
            }
            catch (CustomException customException)
            {
                return StatusCode(customException.ErrorCode, customException.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(501, "Unhandled Exception");
            }
        }

        [HttpPost]
        public IActionResult Post(Cliente clienteNuevo)
        {
            try
            {
                var cliente = _clienteDataService.InsertCliente(clienteNuevo);
                return Ok();
            }
            catch(CustomException customException)
            {
                return StatusCode(customException.ErrorCode, customException.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(501, "Unhandled Exception");
            }
        }

        [HttpPut]
        public IActionResult Put(Cliente clienteModificado)
        {
            try
            {
                var clienteFinal = _clienteDataService.UpdateCliente(clienteModificado);
                if (clienteFinal == null)
                    return NotFound();
                return Ok();
            }
            catch (CustomException customException)
            {
                return StatusCode(customException.ErrorCode, customException.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(501, "Unhandled Exception");
            }
        }

        [HttpDelete]
        public IActionResult Delete(Cliente clienteEliminado)
        {
            try
            {
                var clienteFinal = _clienteDataService.UpdateCliente(clienteEliminado);
                if (clienteFinal == null)
                    return NotFound();

                return Ok();
            }
            catch (CustomException customException)
            {
                return StatusCode(customException.ErrorCode, customException.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(501, "Unhandled Exception");
            }
        }


    }
}
