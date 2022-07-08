using BankAPI.Models;
using BankAPICore.Data;
using Microsoft.AspNetCore.Mvc;

namespace BankAPICore.Controllers.Clients
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteDataService _clienteDataService;
        private readonly IPersonaDataService _personaDataService;

        public ClienteController(IClienteDataService clienteDataService,
            IPersonaDataService personaDataService)
        {
            _clienteDataService = clienteDataService;
            _personaDataService = personaDataService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int idCliente)
        {
            try
            {
                var cliente = await _clienteDataService.GetCliente(idCliente);
                if (cliente == null ||
                   string.IsNullOrEmpty(cliente.Contraseña) ||
                   string.IsNullOrWhiteSpace(cliente.Contraseña))
                    return NotFound();

                return Ok(cliente);
            }
            catch (CustomException customException)
            {
                return StatusCode(customException.ErrorCode, customException.Message);
            }
            catch (Exception)
            {
                return StatusCode(501, "Unhandled Exception");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Cliente clienteNuevo)
        {
            try
            {

                var existingPersona = await _personaDataService.GetPersona(clienteNuevo.IdPersona);
                var insertedPersona = false;
                if (existingPersona != null &&
                   !string.IsNullOrEmpty(existingPersona.Nombre))
                    insertedPersona = _personaDataService.UpdatePersona(clienteNuevo.Persona);
                else
                    insertedPersona = await _personaDataService.InsertPersona(clienteNuevo.Persona);

                if (!insertedPersona)
                    return BadRequest("La persona no pudo ser insertada.");

                var clienteInsertado = await _clienteDataService.InsertCliente(clienteNuevo);
                if (clienteInsertado == false)
                    return BadRequest("El cliente no puedo ser insertado.");

                return Ok(clienteInsertado);
            }
            catch (CustomException customException)
            {
                return StatusCode(customException.ErrorCode, customException.Message);
            }
            catch (Exception)
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
            catch (Exception)
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
            catch (Exception)
            {
                return StatusCode(501, "Unhandled Exception");
            }
        }


    }
}
