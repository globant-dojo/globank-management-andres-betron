using BankAPI.Models;
using BankAPICore.IData;
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
        public async Task<IActionResult> Put(Cliente clienteModificado)
        {
            try
            {
                var clienteExists = await _clienteDataService.GetCliente(clienteModificado.IdCliente);
                if (clienteExists == null ||
                   string.IsNullOrEmpty(clienteExists.Contraseña))
                    return NotFound("El cliente no existe.");

                var clienteFinal = await _clienteDataService.UpdateCliente(clienteModificado);

                return Ok(clienteFinal);
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
        public async Task<IActionResult> Delete(Cliente clienteEliminado)
        {
            try
            {
                var clienteExists =await  _clienteDataService.GetCliente(clienteEliminado.IdCliente);
                if (clienteExists == null ||
                   string.IsNullOrEmpty(clienteExists.Contraseña))
                    return NotFound("El cliente no existe.");

                var clienteFinal = await _clienteDataService.DeleteCliente(clienteExists);
                return Ok(clienteFinal);
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
