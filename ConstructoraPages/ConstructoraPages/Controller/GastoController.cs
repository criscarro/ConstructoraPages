using ConstructoraPages.Dto_s;
using ConstructoraPages.Entity;
using ConstructoraPages.Service;
using Microsoft.AspNetCore.Mvc;

namespace ConstructoraPages.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class GastoController(GastoService gastoService) : ControllerBase
    {
        private readonly GastoService _gastoService = gastoService;

        [HttpGet]
        public ActionResult<List<Gasto>> ObtenerTodos()
        {
            return Ok(_gastoService.ObtenerTodos());
        }

        [HttpGet("{id}")]
        public ActionResult<Gasto> ObtenerPorId(int id)
        {
            var gasto = _gastoService.ObtenerPorId(id);
            return gasto is null ? NotFound() : Ok(gasto);
        }

        [HttpPost]
        public ActionResult<Gasto> Crear(AltaGastoDto dto)
        {
            try
            {
                var gastoCreado = _gastoService.Crear(dto);
                return CreatedAtAction(nameof(ObtenerPorId), new { id = gastoCreado.Id }, gastoCreado);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(new { mensaje = exception.Message });
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Gasto> Modificar(int id, ModificarGastoDto dto)
        {
            try
            {
                var gastoModificado = _gastoService.Modificar(id, dto);
                return gastoModificado is null ? NotFound() : Ok(gastoModificado);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(new { mensaje = exception.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            return _gastoService.Eliminar(id) ? NoContent() : NotFound();
        }
    }
}
