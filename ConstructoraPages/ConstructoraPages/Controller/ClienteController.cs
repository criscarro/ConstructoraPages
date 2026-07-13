using ConstructoraPages.Dto_s;
using ConstructoraPages.Entity;
using ConstructoraPages.Service;
using Microsoft.AspNetCore.Mvc;

namespace ConstructoraPages.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController(ClienteService clienteService) : ControllerBase
    {
        private readonly ClienteService _clienteService = clienteService;

        [HttpGet]
        public ActionResult<List<Cliente>> ObtenerTodos()
        {
            return Ok(_clienteService.ObtenerTodos());
        }

        [HttpGet("{id}")]
        public ActionResult<Cliente> ObtenerPorId(int id)
        {
            var cliente = _clienteService.ObtenerPorId(id);
            return cliente is null ? NotFound() : Ok(cliente);
        }

        [HttpPost]
        public ActionResult<Cliente> Crear(AltaClienteDto dto)
        {
            try
            {
                var clienteCreado = _clienteService.Crear(dto);
                return CreatedAtAction(nameof(ObtenerPorId), new { id = clienteCreado.Id }, clienteCreado);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(new { mensaje = exception.Message });
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Cliente> Modificar(int id, ModificarClienteDto dto)
        {
            try
            {
                var clienteModificado = _clienteService.Modificar(id, dto);
                return clienteModificado is null ? NotFound() : Ok(clienteModificado);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(new { mensaje = exception.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            return _clienteService.Eliminar(id) ? NoContent() : NotFound();
        }
    }
}
